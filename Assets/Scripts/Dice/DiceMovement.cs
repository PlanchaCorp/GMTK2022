using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityAtoms;


public class DiceMovement : MonoBehaviour
{
    private const float ROLL_HEIGHT = 0.25f;

    Dictionary<DiceDirections, Vector3> movementDirections = new Dictionary<DiceDirections, Vector3>()
    {
        { DiceDirections.NONE, Vector3.zero },
        { DiceDirections.TOP, Vector3.right },
        { DiceDirections.RIGHT, Vector3.back },
        { DiceDirections.DOWN, Vector3.left },
        { DiceDirections.LEFT, Vector3.forward }
    };
    Dictionary<DiceDirections, Vector3> rotateDirections = new Dictionary<DiceDirections, Vector3>()
    {
        { DiceDirections.NONE, Vector3.zero },
        { DiceDirections.TOP, Vector3.back },
        { DiceDirections.RIGHT, Vector3.left },
        { DiceDirections.DOWN, Vector3.forward },
        { DiceDirections.LEFT, Vector3.right }
    };

    [SerializeField]
    private AtomBaseVariable<Vector2> playerMovement;
    [SerializeField]
    private AtomEvent<Vector2> onDiceMoveComplete;
    [SerializeField]
    private AtomBaseVariable<float> diceSpeed;
    [SerializeField]
    private AtomBaseVariable<bool> rollTopAllowed;
    [SerializeField]
    private AtomBaseVariable<bool> rollRightAllowed;
    [SerializeField]
    private AtomBaseVariable<bool> rollDownAllowed;
    [SerializeField]
    private AtomBaseVariable<bool> rollLeftAllowed;

    [SerializeField]
    private Transform diceModel;

    private bool isMovementInProgress = false;
    private float currentMovementProgress = 0;
    private DiceDirections currentMovementDirection = DiceDirections.NONE;
    private Vector3 initialPosition;

    private void Start() {
        onDiceMoveComplete.Raise(transform.position);
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (isMovementInProgress) {
            MoveDice();
        }
    }
    
    public void OnPlayerMovement() {
        if (!isMovementInProgress && playerMovement.Value.magnitude > 0) {
            InitMovement();
        }
    }

    private void InitMovement() {
        if (Mathf.Abs(playerMovement.Value.x) > Mathf.Abs(playerMovement.Value.y)) {
            if (playerMovement.Value.x > 0 && rollRightAllowed.Value)
                currentMovementDirection = DiceDirections.RIGHT;
            else if (playerMovement.Value.x < 0 && rollLeftAllowed.Value)
                currentMovementDirection = DiceDirections.LEFT;
        } else if (Mathf.Abs(playerMovement.Value.x) < Mathf.Abs(playerMovement.Value.y)) {
            if (playerMovement.Value.y > 0 && rollTopAllowed.Value)
                currentMovementDirection = DiceDirections.TOP;
            else if (playerMovement.Value.y < 0 && rollDownAllowed.Value)
                currentMovementDirection = DiceDirections.DOWN;
        }

        if (
            (currentMovementDirection == DiceDirections.RIGHT && !rollRightAllowed.Value) ||
            (currentMovementDirection == DiceDirections.TOP && !rollTopAllowed.Value) ||
            (currentMovementDirection == DiceDirections.LEFT && !rollLeftAllowed.Value) ||
            (currentMovementDirection == DiceDirections.DOWN && !rollDownAllowed.Value)
            ) {
                currentMovementDirection = DiceDirections.NONE;
        }
        
        if (currentMovementDirection != DiceDirections.NONE) {
            isMovementInProgress = true;
            currentMovementProgress = 0;
        }
    }

    private void MoveDice() {
        float moveAmount = Mathf.Min(Time.deltaTime * diceSpeed.Value, 1 - currentMovementProgress);
        currentMovementProgress += moveAmount;
        // Move, rotate the dice, and simulate height
        float heightAmount = ((0.5f - Mathf.Abs(currentMovementProgress - 0.5f)) * 2) * ROLL_HEIGHT;
        diceModel.Rotate(rotateDirections[currentMovementDirection] * moveAmount * 90, Space.World);
        transform.Translate(movementDirections[currentMovementDirection] * moveAmount, Space.World);
        transform.position = new Vector3(diceModel.position.x, initialPosition.y + heightAmount, diceModel.position.z);
        onDiceMoveComplete.Raise(transform.position);

        // Stop movement since we reached 1 case
        if (currentMovementProgress >= 1) {
            isMovementInProgress = false;
            // Keep rolling if keys are still pressed
            if (playerMovement.Value.magnitude > 0) {
                InitMovement();
            }
        }
    }
}

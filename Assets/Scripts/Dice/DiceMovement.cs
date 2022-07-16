using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityAtoms;


public class DiceMovement : MonoBehaviour
{
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

    private bool isMovementInProgress = false;
    private float currentMovementProgress = 0;
    private DiceDirections currentMovementDirection = DiceDirections.NONE;

    private void Start() {
        onDiceMoveComplete.Raise(transform.position);
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
        isMovementInProgress = true;
        currentMovementProgress = 0;
        if (Mathf.Abs(playerMovement.Value.x) > Mathf.Abs(playerMovement.Value.y)) {
            currentMovementDirection = playerMovement.Value.x > 0 ? DiceDirections.RIGHT : DiceDirections.LEFT;
        } else if (Mathf.Abs(playerMovement.Value.x) < Mathf.Abs(playerMovement.Value.y)) {
            currentMovementDirection = playerMovement.Value.y > 0 ? DiceDirections.TOP : DiceDirections.DOWN;
        }
    }

    private void MoveDice() {
        float moveAmount = Mathf.Min(Time.deltaTime * diceSpeed.Value, 1 - currentMovementProgress);
        currentMovementProgress += moveAmount;
        transform.Rotate(rotateDirections[currentMovementDirection] * moveAmount * 90, Space.World);
        transform.Translate(movementDirections[currentMovementDirection] * moveAmount, Space.World);
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

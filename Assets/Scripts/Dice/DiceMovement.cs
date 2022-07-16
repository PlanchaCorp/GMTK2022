using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityAtoms;


public class DiceMovement : MonoBehaviour
{
    private const float ROLL_HEIGHT = 0.18f;

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

    [SerializeField] private AtomEvent<int> onDiceMoveComplete;
    [SerializeField] private AtomBaseVariable<Vector2> playerMovement;
    [SerializeField] private AtomBaseVariable<float> diceSpeed;
    [SerializeField] private AtomBaseVariable<float> iceSpeedModifier;
    [SerializeField] private AtomBaseVariable<bool> isOnIce;

    [SerializeField] private AtomBaseVariable<bool> rollTopAllowed;
    [SerializeField] private AtomBaseVariable<bool> rollRightAllowed;
    [SerializeField] private AtomBaseVariable<bool> rollDownAllowed;
    [SerializeField] private AtomBaseVariable<bool> rollLeftAllowed;


    [SerializeField] private Transform diceModel;

    private bool isMovementInProgress = false;
    private float currentMovementProgress = 0;
    private bool isGliding = false;
    private DiceDirections currentMovementDirection = DiceDirections.NONE;
    private Vector3 initialPosition;

    private void Start() {
        onDiceMoveComplete.Raise((int)currentMovementDirection);
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

    private void TryGliding() {
        // Prevent gliding if no tile ahead
        if (
            (currentMovementDirection == DiceDirections.TOP && !rollTopAllowed.Value) ||
            (currentMovementDirection == DiceDirections.RIGHT && !rollRightAllowed.Value) ||
            (currentMovementDirection == DiceDirections.DOWN && !rollDownAllowed.Value) ||
            (currentMovementDirection == DiceDirections.LEFT && !rollLeftAllowed.Value)
        ) {
            currentMovementDirection = DiceDirections.NONE;
        }

        // Start movement
        if (currentMovementDirection != DiceDirections.NONE) {
            isMovementInProgress = true;
            currentMovementProgress = 0;
            isGliding = true;
        }
    }

    private void InitMovement() {
        // Check which move is required by player input
        if (Mathf.Abs(playerMovement.Value.x) > Mathf.Abs(playerMovement.Value.y)) {
            if (playerMovement.Value.x > 0 && rollRightAllowed.Value)
                currentMovementDirection = DiceDirections.RIGHT;
            else if (playerMovement.Value.x < 0 && rollLeftAllowed.Value)
                currentMovementDirection = DiceDirections.LEFT;
            else
                currentMovementDirection = DiceDirections.NONE;
        } else if (Mathf.Abs(playerMovement.Value.x) < Mathf.Abs(playerMovement.Value.y)) {
            if (playerMovement.Value.y > 0 && rollTopAllowed.Value)
                currentMovementDirection = DiceDirections.TOP;
            else if (playerMovement.Value.y < 0 && rollDownAllowed.Value)
                currentMovementDirection = DiceDirections.DOWN;
            else
                currentMovementDirection = DiceDirections.NONE;
        } else if (
            (currentMovementDirection == DiceDirections.RIGHT && playerMovement.Value.x < 0) ||
            (currentMovementDirection == DiceDirections.TOP && playerMovement.Value.y < 0) ||
            (currentMovementDirection == DiceDirections.LEFT && playerMovement.Value.x > 0) ||
            (currentMovementDirection == DiceDirections.DOWN && playerMovement.Value.y > 0)
        ) {
            currentMovementDirection = DiceDirections.NONE;
        }

        // Deny move if no tile ahead
        if (
            (currentMovementDirection == DiceDirections.RIGHT && !rollRightAllowed.Value) ||
            (currentMovementDirection == DiceDirections.TOP && !rollTopAllowed.Value) ||
            (currentMovementDirection == DiceDirections.LEFT && !rollLeftAllowed.Value) ||
            (currentMovementDirection == DiceDirections.DOWN && !rollDownAllowed.Value)
            ) {
                currentMovementDirection = DiceDirections.NONE;
        }
        
        // Start movement
        if (currentMovementDirection != DiceDirections.NONE) {
            isMovementInProgress = true;
            currentMovementProgress = 0;
        }
    }

    private void MoveDice() {
        float speed = isGliding ? diceSpeed.Value * iceSpeedModifier.Value : diceSpeed.Value;
        float moveAmount = Mathf.Min(Time.deltaTime * speed, 1 - currentMovementProgress);
        currentMovementProgress += moveAmount;
        if (!isGliding) {
            // Move, rotate the dice, and simulate height
            float heightAmount = ((0.5f - Mathf.Abs(currentMovementProgress - 0.5f)) * 2) * ROLL_HEIGHT;
            diceModel.Rotate(rotateDirections[currentMovementDirection] * moveAmount * 90, Space.World);
            transform.position = new Vector3(diceModel.position.x, initialPosition.y + heightAmount, diceModel.position.z);
            onDiceMoveComplete.Raise((int)currentMovementDirection);
        }
        transform.Translate(movementDirections[currentMovementDirection] * moveAmount, Space.World);

        // Stop movement since we reached 1 case
        if (currentMovementProgress >= 1) {
            isGliding = false;
            isMovementInProgress = false;
            // Keep gliding if on ice
            if (isOnIce.Value) {
                TryGliding();
            }
            // Keep rolling if keys are still pressed
            if (!isGliding && playerMovement.Value.magnitude > 0) {
                InitMovement();
            }
        }
    }
}

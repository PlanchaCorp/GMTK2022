using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityAtoms;


public class DiceMovement : MonoBehaviour
{
    private const float ROLL_HEIGHT = 0.18f;
    private const float MULTI_ALLOWED_DELAY = 0.025f;

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
    [SerializeField] private AtomBaseVariable<bool> isMoving;

    [SerializeField] private AtomBaseVariable<int> rollTopAllowed;
    [SerializeField] private AtomBaseVariable<int> rollRightAllowed;
    [SerializeField] private AtomBaseVariable<int> rollDownAllowed;
    [SerializeField] private AtomBaseVariable<int> rollLeftAllowed;

    [SerializeField] private AtomBaseVariable<bool> isPauseDisplayed;
    [SerializeField] private AtomBaseVariable<int> dicesMovingCount; 


    [SerializeField] private Transform diceModel;

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
        if (isMoving.Value && !isPauseDisplayed.Value) {
            MoveDice();
        }
    }
    
    public void OnPlayerMovement() {
        if (!isMoving.Value && dicesMovingCount.Value == 0 && playerMovement.Value.magnitude > 0 && !isPauseDisplayed.Value) {
            TryMovement();
        }
    }

    private void TryGliding() {
        // Prevent gliding if no tile ahead
        if (
            (currentMovementDirection == DiceDirections.TOP && rollTopAllowed.Value <= 0) ||
            (currentMovementDirection == DiceDirections.RIGHT && rollRightAllowed.Value <= 0) ||
            (currentMovementDirection == DiceDirections.DOWN && rollDownAllowed.Value <= 0) ||
            (currentMovementDirection == DiceDirections.LEFT && rollLeftAllowed.Value <= 0)
        ) {
            currentMovementDirection = DiceDirections.NONE;
        }

        // Start movement
        if (currentMovementDirection != DiceDirections.NONE) {
            InitMovement(true);
            isGliding = true;
        }
    }

    private void TryMovement(bool isSuccessive = false) {
        // Check which move is required by player input
        if (Mathf.Abs(playerMovement.Value.x) > Mathf.Abs(playerMovement.Value.y)) {
            if (playerMovement.Value.x > 0 && rollRightAllowed.Value > 0)
                currentMovementDirection = DiceDirections.RIGHT;
            else if (playerMovement.Value.x < 0 && rollLeftAllowed.Value > 0)
                currentMovementDirection = DiceDirections.LEFT;
            else
                currentMovementDirection = DiceDirections.NONE;
        } else if (Mathf.Abs(playerMovement.Value.x) < Mathf.Abs(playerMovement.Value.y)) {
            if (playerMovement.Value.y > 0 && rollTopAllowed.Value > 0)
                currentMovementDirection = DiceDirections.TOP;
            else if (playerMovement.Value.y < 0 && rollDownAllowed.Value > 0)
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
            (currentMovementDirection == DiceDirections.RIGHT && rollRightAllowed.Value <= 0) ||
            (currentMovementDirection == DiceDirections.TOP && rollTopAllowed.Value <= 0) ||
            (currentMovementDirection == DiceDirections.LEFT && rollLeftAllowed.Value <= 0) ||
            (currentMovementDirection == DiceDirections.DOWN && rollDownAllowed.Value <= 0)
            ) {
                currentMovementDirection = DiceDirections.NONE;
        }
        
        // Start movement
        if (currentMovementDirection != DiceDirections.NONE) {
            InitMovement(isSuccessive);
        }
    }

    private void InitMovement(bool isSuccessive) {
        isMoving.Value = true;
        currentMovementProgress = 0;
        if (isSuccessive)
            dicesMovingCount.Value++;
        else
            StartCoroutine("IncreaseDiceMovingCount");
    }
    private IEnumerator IncreaseDiceMovingCount() {
        yield return new WaitForSeconds(MULTI_ALLOWED_DELAY);
        dicesMovingCount.Value++;
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
        }
        transform.Translate(movementDirections[currentMovementDirection] * moveAmount, Space.World);

        // Stop movement since we reached 1 case
        if (currentMovementProgress >= 1) {
            // bool wasGliding = isGliding;
            isGliding = false;
            isMoving.Value = false;
            dicesMovingCount.Value--;
            // Keep gliding if on ice
            if (isOnIce.Value) {
                TryGliding();
            }
            if (!isGliding) {
                onDiceMoveComplete.Raise((int)currentMovementDirection);
            }
        }
    }
}

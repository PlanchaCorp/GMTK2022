using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityAtoms;


public class DiceMovement : MonoBehaviour
{
    // The dice center lifts up a little while rolling
    private const float ROLL_HEIGHT = 0.18f;
    [SerializeField] private AtomBaseVariable<float> DICE_SPEED;
    [SerializeField] private AtomBaseVariable<float> ICE_SPEED_MODIFIER;

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
    
    [SerializeField] private AtomEvent<bool> onDiceMoveChange;
    [SerializeField] private AtomEvent<int> onMoveRequested;
    [SerializeField] private AtomBaseVariable<bool> sensorTopReachable;
    [SerializeField] private AtomBaseVariable<bool> sensorRightReachable;
    [SerializeField] private AtomBaseVariable<bool> sensorDownReachable;
    [SerializeField] private AtomBaseVariable<bool> sensorLeftReachable;
    [SerializeField] private AtomBaseVariable<bool> isOnIce;

    [SerializeField] private Transform diceModel;


    private bool isMoving = false;
    private float currentMovementProgress = 0;
    private bool isGliding = false;

    private DiceDirections currentMovementDirection = DiceDirections.NONE;
    private Vector3 initialPosition;

    private void Awake() {
    }

    private void Start() {
        initialPosition = transform.position;
        onMoveRequested.Register(direction => this.OnMoveRequested(direction));
    }

    private void OnDestroy() {
        onMoveRequested.Unregister(direction => this.OnMoveRequested(direction));
    }

    private void Update()
    {
        if (isMoving && Time.timeScale > 0)
            MoveDice();
    }

    private bool OnMoveRequested(int directionRequested, bool acceptMovingDice = false) {
        DiceDirections direction = (DiceDirections)directionRequested;
        if (
            (isMoving && !acceptMovingDice) ||
            (direction == DiceDirections.TOP && !sensorTopReachable.Value) ||
            (direction == DiceDirections.RIGHT && !sensorRightReachable.Value) ||
            (direction == DiceDirections.DOWN && !sensorDownReachable.Value) ||
            (direction == DiceDirections.LEFT && !sensorLeftReachable.Value)
        )
            return false;
        
        currentMovementDirection = direction;
        currentMovementProgress = 0;

        if (!isMoving)
            onDiceMoveChange.Raise(true);
        isMoving = true;

        return true;
    }

    private void MoveDice() {
        float speed = isGliding ? DICE_SPEED.Value * ICE_SPEED_MODIFIER.Value : DICE_SPEED.Value;
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
            bool keepMoving = false;

            // Keep gliding if on ice
            if (isOnIce.Value) {
                keepMoving = OnMoveRequested((int)currentMovementDirection, true);
                if (keepMoving)
                    isGliding = true;
            }

            if (!keepMoving) {
                isMoving = false;
                isGliding = false;
                onDiceMoveChange.Raise(false);
            }
        }
    }
}

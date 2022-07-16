using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms;
using UnityEngine.UI;


enum DiceDirection {
    NONE, TOP, RIGHT, DOWN, LEFT
}

public class DiceMovement : MonoBehaviour
{
    Dictionary<DiceDirection, Vector3> movementDirections = new Dictionary<DiceDirection, Vector3>()
    {
        { DiceDirection.NONE, Vector3.zero },
        { DiceDirection.TOP, Vector3.right },
        { DiceDirection.RIGHT, Vector3.back },
        { DiceDirection.DOWN, Vector3.left },
        { DiceDirection.LEFT, Vector3.forward }
    };
    Dictionary<DiceDirection, Vector3> rotateDirections = new Dictionary<DiceDirection, Vector3>()
    {
        { DiceDirection.NONE, Vector3.zero },
        { DiceDirection.TOP, Vector3.back },
        { DiceDirection.RIGHT, Vector3.left },
        { DiceDirection.DOWN, Vector3.forward },
        { DiceDirection.LEFT, Vector3.right }
    };

    [SerializeField]
    private UnityAtoms.AtomBaseVariable<Vector2> playerMovement;
    [SerializeField]
    private UnityAtoms.AtomBaseVariable<float> diceSpeed;

    private bool isMovementInProgress = false;
    private float currentMovementProgress = 0;
    private DiceDirection currentMovementDirection = DiceDirection.NONE;
    
    public void OnPlayerMovement() {
        if (!isMovementInProgress && playerMovement.Value.magnitude > 0) {
            InitMovement();
        }
    }

    private void InitMovement() {
        isMovementInProgress = true;
        currentMovementProgress = 0;
        if (Mathf.Abs(playerMovement.Value.x) > Mathf.Abs(playerMovement.Value.y)) {
            currentMovementDirection = playerMovement.Value.x > 0 ? DiceDirection.RIGHT : DiceDirection.LEFT;
        } else if (Mathf.Abs(playerMovement.Value.x) < Mathf.Abs(playerMovement.Value.y)) {
            currentMovementDirection = playerMovement.Value.y > 0 ? DiceDirection.TOP : DiceDirection.DOWN;
        }
    }

    void Update()
    {
        if (isMovementInProgress) {
            MoveDice();
        }
    }

    private void MoveDice() {
        float moveAmount = Mathf.Min(Time.deltaTime * diceSpeed.Value, 1 - currentMovementProgress);
        currentMovementProgress += moveAmount;
        transform.Rotate(rotateDirections[currentMovementDirection] * moveAmount * 90, Space.World);
        transform.Translate(movementDirections[currentMovementDirection] * moveAmount, Space.World);

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms;
using UnityAtoms.FSM;


public class DicesController : MonoBehaviour
{

    [SerializeField] private FiniteStateMachineReference dicesState;
    [SerializeField] private AtomEvent<string> onDiceStateChange;

    [SerializeField] private AtomEvent<bool> onDiceMoveChange;
    [SerializeField] private AtomEvent<int> onMoveRequested;
    [SerializeField] private AtomEvent<Vector2> onPlayerMovement;
    [SerializeField] private AtomBaseVariable<Vector2> playerMovement;

    private int diceMovingCount = 0;
    private DiceDirections previousPlayerDirection;

    private void Start() {
        onPlayerMovement.Register(this.OnPlayerInput);
        onDiceMoveChange.Register(this.OnDiceMoveChange);
        onDiceStateChange.Register(this.OnDiceStateChange);

        dicesState.Machine.DispatchWhen(command: "BeginMovement", (value) => value == "Idle" && diceMovingCount > 0, gameObject);
        dicesState.Machine.DispatchWhen(command: "EndMovement", (value) => value == "Moving" && diceMovingCount == 0, gameObject);
        // dicesState.Machine.DispatchWhen(command: "LockDices", (value) => value == "Moving" && diceMovingCount > 0, gameObject);
    }
    private void OnDestroy() {
        onPlayerMovement.Unregister(this.OnPlayerInput);
        onDiceMoveChange.Unregister(this.OnDiceMoveChange);
        onDiceStateChange.Unregister(this.OnDiceStateChange);
    }

    private void OnDiceMoveChange(bool isMoving) {
        diceMovingCount += isMoving ? 1 : -1;
    }

    private void OnDiceStateChange(string state) {
        if (state == DicesStates.Idle)
            OnPlayerInput(playerMovement.Value);
    }

    public void OnPlayerInput(Vector2 input) {
        if (dicesState.Machine.Value != DicesStates.Idle || input.magnitude == 0)
            return;

        DiceDirections requestedDirection = DiceDirections.NONE;
        if (
            input.magnitude > 0
        ) {
            if (Mathf.Abs(input.x) > Mathf.Abs(input.y)) {
                if (input.x > 0)
                    requestedDirection = DiceDirections.RIGHT;
                else if (input.x < 0)
                    requestedDirection = DiceDirections.LEFT;
            } else if (Mathf.Abs(input.x) < Mathf.Abs(input.y)) {
                if (input.y > 0)
                    requestedDirection = DiceDirections.TOP;
                else if (input.y < 0)
                    requestedDirection = DiceDirections.DOWN;
            } else if (
                (previousPlayerDirection == DiceDirections.RIGHT && input.x > 0) ||
                (previousPlayerDirection == DiceDirections.LEFT && input.x < 0) ||
                (previousPlayerDirection == DiceDirections.TOP && input.y > 0) ||
                (previousPlayerDirection == DiceDirections.DOWN && input.y < 0)
            )
                requestedDirection = previousPlayerDirection;
        }
        if (requestedDirection != DiceDirections.NONE)
            onMoveRequested.Raise((int)requestedDirection);
    }
}

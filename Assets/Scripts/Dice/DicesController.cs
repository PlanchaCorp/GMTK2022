using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms;
using UnityAtoms.FSM;
using UniRx;


public class DicesController : MonoBehaviour
{

    [SerializeField] private FiniteStateMachine dicesState;
    [SerializeField] private FiniteStateMachine levelState;

    [SerializeField] private AtomEvent<bool> onDiceMoveChange;
    [SerializeField] private AtomEvent<int> onMoveRequested;
    [SerializeField] private AtomEvent<Vector2> onPlayerMovement;
    [SerializeField] private AtomBaseVariable<Vector2> playerMovement;
    [SerializeField] private AtomBaseVariable<int> diceMovesCount;

    private int diceMovingCount = 0;
    private DiceDirections previousPlayerDirection = DiceDirections.NONE;

    private void Start() {
        onPlayerMovement.Observe()
            .TakeUntilDestroy(this)
            .Subscribe(this.OnPlayerInput);
        onDiceMoveChange.Observe()
            .TakeUntilDestroy(this)
            .Subscribe(this.OnDiceMoveChange);
    }

    private void OnDestroy() {
        dicesState.Reset();
    }

    private void OnDiceMoveChange(bool isMoving) {
        if (isMoving && diceMovingCount == 0)
            diceMovesCount.Value++;

        diceMovingCount = Mathf.Max(diceMovingCount + (isMoving ? 1 : -1), 0);

        if (dicesState.Value == DicesStates.Idle && diceMovingCount > 0)
            dicesState.Dispatch(DicesTransitions.BeginMovement);
        else if (dicesState.Value == DicesStates.Moving && diceMovingCount == 0) {
            dicesState.Dispatch(DicesTransitions.EndMovement);
            OnPlayerInput(playerMovement.Value);
        }
    }

    public void OnPlayerInput(Vector2 input) {
        if (dicesState.Value != DicesStates.Idle || levelState.Value != LevelStates.InProgress || input.magnitude == 0)
            return;

        DiceDirections requestedDirection = DiceDirections.NONE;
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

        previousPlayerDirection = requestedDirection;
        if (requestedDirection != DiceDirections.NONE) {
            onMoveRequested.Raise((int)requestedDirection);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityAtoms;
using UnityAtoms.FSM;


public class DicesController : MonoBehaviour
{

    [SerializeField] private FiniteStateMachine dicesState;
    [SerializeField] private AtomEvent<string> onDiceStateChange;

    [SerializeField] private AtomEvent<bool> onDiceMoveChange;
    [SerializeField] private AtomEvent<int> onMoveRequested;
    [SerializeField] private AtomEvent<Vector2> onPlayerMovement;
    [SerializeField] private AtomBaseVariable<Vector2> playerMovement;

    private int diceMovingCount = 0;
    private DiceDirections previousPlayerDirection = DiceDirections.NONE;

    private void Awake() {
    }

    private void Start() {
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
        onPlayerMovement.Register(this.OnPlayerInput);
        onDiceMoveChange.Register(this.OnDiceMoveChange);
        onDiceStateChange.Register(this.OnDiceStateChange);
        dicesState.DispatchWhen(command: DicesTransitions.BeginMovement, (value) => value == DicesStates.Idle && diceMovingCount > 0, gameObject);
        dicesState.DispatchWhen(command: DicesTransitions.EndMovement, (value) => value == DicesStates.Moving && diceMovingCount == 0, gameObject);
        // dicesState.Machine.DispatchWhen(command: "LockDices", (value) => value == "Moving" && diceMovingCount > 0, gameObject);
    }

    private void OnActiveSceneChanged(Scene previousScene, Scene nextScene) {
        dicesState.Reset();
        playerMovement.Reset();
    }

    private void OnDestroy() {
        onPlayerMovement.Unregister(this.OnPlayerInput);
        onDiceMoveChange.Unregister(this.OnDiceMoveChange);
        onDiceStateChange.Unregister(this.OnDiceStateChange);
        Debug.Log("OnDestroy");
    }

    private void OnDiceStateChange(string state) {
        if (state == DicesStates.Idle)
            OnPlayerInput(playerMovement.Value);
    }

    private void OnDiceMoveChange(bool isMoving) {
        Debug.Log("OnDiceMoveChange");
        diceMovingCount = Mathf.Max(diceMovingCount + (isMoving ? 1 : -1), 0);
    }

    public void OnPlayerInput(Vector2 input) {
        Debug.Log("OnPlayerInput " + DicesStates.Idle);
        if (dicesState.Value != DicesStates.Idle || input.magnitude == 0)
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
        if (requestedDirection != DiceDirections.NONE)
            onMoveRequested.Raise((int)requestedDirection);
    }
}

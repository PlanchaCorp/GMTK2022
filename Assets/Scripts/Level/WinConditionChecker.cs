using UnityEngine;
using UnityAtoms;
using UniRx;
using UnityAtoms.FSM;

public class WinConditionChecker : MonoBehaviour {
    [SerializeField]
    private AtomEvent<bool> goalChangedEvent;
    [SerializeField] 
    private FiniteStateMachineReference levelState;

    private int currentReachedGoalCount = 0;
    private int diceCount;

    void Awake(){
        diceCount = GameObject.FindGameObjectsWithTag("Dice").Length;
        goalChangedEvent.Observe()
            .TakeUntilDestroy(this)
            .Subscribe(OnGoalChanged);

    }
    
    private void OnGoalChanged(bool isCorrectlyPressed) {
        currentReachedGoalCount += isCorrectlyPressed ? 1 : -1;
        if (currentReachedGoalCount == diceCount)
            levelState.Machine.Dispatch(LevelTransition.Complete);
    }

}
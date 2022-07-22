using UnityEngine;
using UnityAtoms;
using UniRx;
using UnityAtoms.FSM;

public class WinConditionChecker : MonoBehaviour {

    [SerializeField]
    private int diceCount;
    private int goalCount;
    [SerializeField]
    private AtomEvent<bool> goalChangedEvent;
    [SerializeField] 
    private FiniteStateMachineReference levelState;
    private int currentReachedGoalCount=0;

    void Awake(){
        goalChangedEvent.Observe()
            .TakeUntilDestroy(this)
            .Subscribe(OnGoalChanged);

    }
    
    private void OnGoalChanged(bool isCorrectlyPressed) {
        currentReachedGoalCount += isCorrectlyPressed ? 1 : -1;
        if (currentReachedGoalCount == goalCount)
            levelState.Machine.Dispatch(LevelTransition.Complete);
    }

}
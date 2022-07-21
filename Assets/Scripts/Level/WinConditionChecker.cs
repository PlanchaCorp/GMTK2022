using UnityEngine;
using UnityAtoms;
using UniRx;
using UnityAtoms.FSM;

public class WinConditionChecker : MonoBehaviour {

    [SerializeField]
    private SceneDispatcher dataBase;
    private int numberOfEnd;
    [SerializeField]
    private AtomEvent<bool> goalChangedEvent;
    [SerializeField] 
    private FiniteStateMachineReference levelState;
    private int currentReachedGoalCount=0;

    void Awake(){
        numberOfEnd = 1;
        goalChangedEvent.Observe()
            .TakeUntilDestroy(this)
            .Subscribe(OnGoalChanged);

    }
    
    private void OnGoalChanged(bool isCorrectlyPressed) {
        currentReachedGoalCount += isCorrectlyPressed ? 1 : -1;
        if (currentReachedGoalCount == numberOfEnd)
            levelState.Machine.Dispatch(LevelTransition.Complete);
    }

}
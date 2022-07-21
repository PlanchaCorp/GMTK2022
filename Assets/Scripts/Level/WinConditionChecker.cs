using UnityEngine;
using UnityAtoms;
using UniRx;
using UnityAtoms.FSM;

public class WinConditionChecker : MonoBehaviour {

    [SerializeField]
    private SceneDataBase dataBase;
    private int numberOfEnd;
    [SerializeField]
    private AtomEvent<bool> goalChangedEvent;
    [SerializeField] 
    private FiniteStateMachineReference levelState;
    private int currentReachedGoalCount=0;

    void Awake(){
        Debug.Log( dataBase.getCurrentLevel());
        numberOfEnd = dataBase.getCurrentLevel().numberOfEnd;
        Observable.AsObservable(goalChangedEvent.Observe())
        .TakeUntilDestroy(this)
        .Subscribe(OnGoalChanged);

    }
    
    private void OnGoalChanged(bool isCorrectlyPressed) {
        currentReachedGoalCount += isCorrectlyPressed ? 1 : -1;
        if (currentReachedGoalCount == numberOfEnd)
            levelState.Machine.Dispatch(LevelTransition.Complete);
    }

}
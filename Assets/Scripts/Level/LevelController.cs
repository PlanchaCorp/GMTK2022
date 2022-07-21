using UnityEngine;
using UnityEngine.SceneManagement;
using UnityAtoms;
using UnityAtoms.FSM;
using UniRx;

public class LevelController : MonoBehaviour
{
    [SerializeField] private SceneDataBase sceneDB;
    [SerializeField] private FiniteStateMachineReference levelState;

    [SerializeField] private AtomEvent<string> onLevelStateChange;

    private int levelGoalTotalCount;
    private int currentReachedGoalCount = 0;

    private void Awake() {
        levelState.Machine.ObserveEveryValueChanged(x=> x.Value, FrameCountType.Update)
        .TakeUntilDestroy(this)
        .Subscribe(OnLevelStateChange);
        
    }

    private void OnDestroy() {
        levelState.Machine.Reset();
    }

    private void OnLevelStateChange(string state) {
        if (state == LevelStates.Completed) {
            Time.timeScale = 0;
        } else if (state == LevelStates.Paused) {
            Time.timeScale = 0;
        } else if (state == LevelStates.InProgress) {
            Time.timeScale = 1;
        }
    }
}

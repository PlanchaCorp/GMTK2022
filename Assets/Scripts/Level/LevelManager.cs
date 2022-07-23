using UnityEngine;
using UnityAtoms;
using UnityAtoms.FSM;
using UniRx;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private SceneDispatcher sceneDispatcher;
    [SerializeField] private FiniteStateMachine levelState;

    [SerializeField] private AtomEvent<string> onLevelStateChange;
    [SerializeField] private AtomEvent<Void> onRestartRequest;
    [SerializeField] private AtomEvent<Void> onNextLevelRequest;
    [SerializeField] private AtomEvent<Void> onMainMenuRequest;
    [SerializeField] private AtomEvent<Void> onPauseRequest;

    private void Awake() {
        Time.timeScale = 1;
        sceneDispatcher.VerifyScene();
        onLevelStateChange.Observe()
            .TakeUntilDestroy(this)
            .Subscribe(OnLevelStateChange);
        onRestartRequest.Observe()
            .TakeUntilDestroy(this)
            .Subscribe(_ => sceneDispatcher.LoadSameLevel());
        onNextLevelRequest.Observe()
            .TakeUntilDestroy(this)
            .Subscribe(_ => sceneDispatcher.LoadNextLevel());
        onMainMenuRequest.Observe()
            .TakeUntilDestroy(this)
            .Subscribe(_ => sceneDispatcher.LoadMainMenu());
        onPauseRequest.Observe()
            .TakeUntilDestroy(this)
            .Subscribe(_ => this.OnPauseRequest());
        
    }

    private void OnDestroy() {
        levelState.Reset();
        Time.timeScale = 1;
    }

    private void OnPauseRequest(){
        if(levelState.Value == LevelStates.InProgress){
            levelState.Dispatch(LevelTransition.Pause);
        } else if (levelState.Value == LevelStates.Paused){
            levelState.Dispatch(LevelTransition.Unpause);
        }
    }

    private void OnLevelStateChange(string state) {
        if (state == LevelStates.Completed) {
            Time.timeScale = 1;
        } else if (state == LevelStates.Paused) {
            Time.timeScale = 0;
        } else if (state == LevelStates.InProgress) {
            Time.timeScale = 1;
        }
    }
}

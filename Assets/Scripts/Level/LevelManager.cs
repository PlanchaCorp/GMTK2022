using UnityEngine;
using UnityEngine.SceneManagement;
using UnityAtoms;
using UnityAtoms.FSM;
using UniRx;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private SceneDispatcher sceneDispatcher;
    [SerializeField] private FiniteStateMachineReference levelState;

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

    private void OnDestroy(){
        levelState.Machine.Reset();
    }

    private void OnPauseRequest(){
        if(levelState.Machine.Value == LevelStates.InProgress){
            levelState.Machine.Dispatch(LevelTransition.Pause);
        } else if (levelState.Machine.Value == LevelStates.Paused){
            levelState.Machine.Dispatch(LevelTransition.Unpause);
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

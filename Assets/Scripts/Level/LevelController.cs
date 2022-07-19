using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityAtoms;
using UnityAtoms.FSM;

public class LevelController : MonoBehaviour
{
    [SerializeField] private FiniteStateMachineReference levelState;

    [SerializeField] private AtomEvent<bool> onGoalChange;
    [SerializeField] private AtomEvent<string> onLevelStateChange;
    [SerializeField] private AtomEvent<Void> onLevelComplete; 

    private int levelGoalTotalCount;
    private int currentReachedGoalCount = 0;

    private void Awake() {
    }

    private void Start()
    {
        levelGoalTotalCount = GameObject.FindGameObjectsWithTag("Goal").Length;
        onGoalChange.Register(this.OnGoalChanged);
        onLevelStateChange.Register(this.OnLevelStateChange);

    }

    private void OnDestroy() {
        Debug.Log("OnDestroy level");
        onGoalChange.Unregister(this.OnGoalChanged);
        onLevelStateChange.Unregister(this.OnLevelStateChange);
        // levelState.Machine.Reset();
    }

    private void OnGoalChanged(bool isCorrectlyPressed) {
        currentReachedGoalCount += isCorrectlyPressed ? 1 : -1;
        if (currentReachedGoalCount == levelGoalTotalCount)
            levelState.Machine.Dispatch(LevelTransition.Complete);
    }

    public void TogglePause() {
        if (levelState.Machine.Value == LevelStates.InProgress)
            levelState.Machine.Dispatch(LevelTransition.Pause);
        else
            levelState.Machine.Dispatch(LevelTransition.Unpause);
    }

    public void Restart() {
        Debug.Log("Restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnLevelStateChange(string state) {
        if (state == LevelStates.Completed) {
            onLevelComplete.Raise();
        } else if (state == LevelStates.Paused) {
            Time.timeScale = 0;
        } else if (state == LevelStates.InProgress) {
            Time.timeScale = 1;
        }
    }
}

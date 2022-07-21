using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityAtoms;
using UnityAtoms.BaseAtoms;

public class LevelManager : MonoBehaviour
{
    [SerializeField] public string[] meadowLevels;
    [SerializeField] public string[] tundraLevels;
    [SerializeField] public string[] beachLevels;
    [SerializeField] private string mainMenuScene;

    [SerializeField] private AtomEvent<Void> allGoalReached;
    [SerializeField] private AtomBaseVariable<bool> isEndLevelTriggered;
    [SerializeField]
    private AtomBaseVariable<int> hitCount;

    private string currentSceneName;
    private string nextSceneName;
    private int totalGoal;

    private void Start() {
        hitCount.Value = 0;
        isEndLevelTriggered.Value = false;
        currentSceneName = SceneManager.GetActiveScene().name;
        nextSceneName = GetNextSceneName();
        totalGoal = GameObject.FindGameObjectsWithTag("Goal").Length;
    }

    private string GetNextSceneName() {
        return "M_Camel";
    }

    public void GoToNextLevel() {
        Debug.Log("Loading " + nextSceneName);
        SceneManager.LoadScene(nextSceneName);
    }
        public void GoToMenuLevel() {
        SceneManager.LoadScene(mainMenuScene);
    }

    public void RestartLevel(bool force) {
        if (force || !isEndLevelTriggered.Value) {
            SceneManager.LoadScene(currentSceneName);
        }
    }

    public void OnGoalReachedChange(int goalCount) {
        if (goalCount == totalGoal) {
            isEndLevelTriggered.Value = true;
            allGoalReached.Raise();
        }
    }
    public void LoadSceneByName(string name) {
        SceneManager.LoadScene(name);
    }
}

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
        // int i = 0;
        // foreach(string meadowLevel in meadowLevels) {
        //     if (meadowLevel == currentSceneName)
        //         return i + 1 < meadowLevels.Length ? meadowLevels[i + 1] : mainMenuScene;
        //     i++;
        // }
        // i = 0;
        // foreach(string tundraLevel in tundraLevels) {
        //     if (tundraLevel == currentSceneName)
        //         return i + 1 < tundraLevels.Length ? tundraLevels[i + 1] : mainMenuScene;
        //     i++;
        // }
        // i = 0;
        // foreach(string beachLevel in beachLevels) {
        //     if (beachLevel == currentSceneName)
        //         return i + 1 < beachLevels.Length ? beachLevels[i + 1] : mainMenuScene;
        //     i++;
        // }
        return "MainMenu";
    }

    public void GoToNextLevel() {
        Debug.Log("Loading " + nextSceneName);
        SceneManager.LoadScene(nextSceneName);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityAtoms;
using UnityAtoms.BaseAtoms;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private string[] meadowLevels;
    [SerializeField] private string[] tundraLevels;
    [SerializeField] private string[] beachLevels;
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
        int i = 0;
        foreach(string meadowLevel in meadowLevels) {
            if (meadowLevel == currentSceneName)
                return i + 1 < meadowLevels.Length ? meadowLevels[i + 1] : mainMenuScene;
            i++;
        }
        foreach(string tundraLevel in tundraLevels) {
            if (tundraLevel == currentSceneName)
                return i + 1 < tundraLevels.Length ? tundraLevels[i + 1] : mainMenuScene;
            i++;
        }
        foreach(string beachLevel in beachLevels) {
            if (beachLevel == currentSceneName)
                return i + 1 < beachLevels.Length ? beachLevels[i + 1] : mainMenuScene;
            i++;
        }
        return mainMenuScene;
    }

    public void GoToNextLevel() {
        SceneManager.LoadScene(nextSceneName);
    }

    public void RestartLevel() {
        SceneManager.LoadScene(currentSceneName);
    }

    public void OnGoalReachedChange(int goalCount) {
        if (goalCount == totalGoal) {
            Debug.Log("Win!");
            isEndLevelTriggered.Value = true;
            allGoalReached.Raise();
        }
    }
}

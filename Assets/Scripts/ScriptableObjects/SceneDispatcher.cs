using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SceneDispatcher", menuName = "Scene Data/Scene Dispatcher")]
public class SceneDispatcher : ScriptableObject
{
    public List<World> worlds;
    public string menuSceneName;
    private int currentLevelIndex = 0;
    private int currentWorldIndex = 0;

    public void LoadLevelWithIndex(int worldIndex,int levelIndex)
    {
        if (worldIndex < worlds.Count
        && levelIndex < worlds[worldIndex].levels.Count)
        {
            currentLevelIndex = levelIndex;
            currentWorldIndex = worldIndex;
            Debug.Log("Loading level ["+worldIndex + "-" + levelIndex + "]");
            SceneManager.LoadScene(worlds[currentWorldIndex].levels[currentLevelIndex].sceneName);
        }
        else {
            Debug.Log("No scene found for index ["+worldIndex + "-" + levelIndex + "]. Returning to menu");
            LoadMainMenu();
        }
    }
    public void LoadNextLevel()
    {
        LoadLevelWithIndex(currentWorldIndex,currentLevelIndex + 1);
    }
    public void LoadSameLevel()
    {
        LoadLevelWithIndex(currentWorldIndex,currentLevelIndex);
    }
    public void LoadNewGame()
    {
        LoadLevelWithIndex(0,0);
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(menuSceneName);
    }
}
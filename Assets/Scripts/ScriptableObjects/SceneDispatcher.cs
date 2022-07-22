using System.Linq;
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

    public void VerifyScene() {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (worlds[currentWorldIndex].levels[currentLevelIndex].sceneName != currentSceneName) {
            int worldId = 0;
            int levelId = 0;
            foreach (World world in worlds) {
                foreach (Level level in world.levels) {
                    if (level.sceneName == currentSceneName) {
                        currentWorldIndex = worldId;
                        currentLevelIndex = levelId;
                        Debug.Log("Fixing current level id to [" + worldId + "-" + levelId + "]");
                        return;
                    }
                    levelId++;
                }
                levelId = 0;
                worldId++;
            }
        }
    }
}
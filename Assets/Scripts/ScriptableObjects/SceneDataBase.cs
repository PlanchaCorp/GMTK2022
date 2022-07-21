using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SceneDataBase", menuName = "Scene Data/Database")]
public class SceneDataBase : ScriptableObject
{
    public List<Level> levels = new List<Level>();
    public string menu;
    public int CurrentLevelIndex=0;

    /*
     * Levels
     */

    //Load a scene with a given index
    public void LoadLevelWithIndex(int index)
    {
        if (index < levels.Count)
        {
            SceneManager.LoadScene(index);
        }
        //reset the index if we have no more levels
        else CurrentLevelIndex =0;
    }
    //Start next level
    public void NextLevel()
    {
        CurrentLevelIndex++;
        LoadLevelWithIndex(CurrentLevelIndex);
    }
    //Restart current level
    public void RestartLevel()
    {
        LoadLevelWithIndex(CurrentLevelIndex);
    }
    //New game, load level 1
    public void NewGame()
    {
        CurrentLevelIndex= 0;
        LoadLevelWithIndex(CurrentLevelIndex);
    }

    /*
     * Menus
     */

    //Load main Menu
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(menu);
    }
    public Level getCurrentLevel(){
        return levels[CurrentLevelIndex];
    }
}
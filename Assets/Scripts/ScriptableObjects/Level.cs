using UnityEngine;

[CreateAssetMenu(fileName = "NewLevel", menuName = "Scene Data/Level")]
public class Level : ScriptableObject
{
    public string sceneName;
    public string shortDescription;

}
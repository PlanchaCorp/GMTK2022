using UnityEngine;

[CreateAssetMenu(fileName = "NewLevel", menuName = "Scene Data/Level")]
public class Level : GameScene
{
    public enum World {MEADOW,TUNDRA,BEACH}
    //Settings specific to level only
    [Header("Level specific")]
    public int numberOfEnd;
    public World world;
    public int cameraSize;
    public Color LightColor;
}
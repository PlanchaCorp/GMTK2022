using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWorld", menuName = "Scene Data/World")]
public class World : ScriptableObject
{
    public string worldName;
    public List<Level> levels;
    [Header("Sounds")]
    public AudioClip music;
    [Header("Light")]
    public Color lightColor;
}
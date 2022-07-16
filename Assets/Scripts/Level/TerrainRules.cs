using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityAtoms;

public class TerrainRules : MonoBehaviour
{
    [SerializeField]
    private AtomBaseVariable<bool> rollTopAllowed;
    [SerializeField]
    private AtomBaseVariable<bool> rollRightAllowed;
    [SerializeField]
    private AtomBaseVariable<bool> rollDownAllowed;
    [SerializeField]
    private AtomBaseVariable<bool> rollLeftAllowed;
    [SerializeField]
    private Tilemap tilemap;

    public void UpdateDicePossibleMovements(Vector2 dicePosition) {
        List<DiceDirections> directions = new List<DiceDirections>();
        // Debug.Log(tilemap.size + " - " + tilemap.cellBounds.x + "-" + tilemap.cellBounds.y + "-" + tilemap.cellBounds.z);
        // Debug.Log(tilemap.origin);
        Debug.Log(tilemap.GetInstantiatedObject(new Vector3Int(0, 0, 0)));
        Debug.Log(tilemap.GetInstantiatedObject(new Vector3Int(0, 2, 0)));
        Debug.Log(tilemap.HasTile(new Vector3Int(0, 0, 0)));
        Debug.Log(tilemap.HasTile(new Vector3Int(0, 2, 0)));
        // Debug.Log(tilemap.GetCellCenterLocal(new Vector3Int(0, 0, 0)));
        // Debug.Log(tilemap.GetCellCenterWorld(new Vector3Int(0, 0, 0)));
        // Debug.Log(tilemap.GetInstantiatedObject(new Vector3Int(1, 0, 0)));
        // Debug.Log(tilemap.GetInstantiatedObject(new Vector3Int(2, 0, 0)));
    }
}

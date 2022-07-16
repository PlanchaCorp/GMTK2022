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

    private int topSideCollisionCount = 0;
    private int rightSideCollisionCount = 0;
    private int downSideCollisionCount = 0;
    private int leftSideCollisionCount = 0;

    public void UpdateTopMoveAvailability(bool available) {
        topSideCollisionCount += available ? 1 : -1;
        rollTopAllowed.Value = topSideCollisionCount > 0;
    }

    public void UpdateRightMoveAvailability(bool available) {
        rightSideCollisionCount += available ? 1 : -1;
        rollRightAllowed.Value = rightSideCollisionCount > 0;
    }

    public void UpdateDownMoveAvailability(bool available) {
        downSideCollisionCount += available ? 1 : -1;
        rollDownAllowed.Value = downSideCollisionCount > 0;
    }

    public void UpdateLeftMoveAvailability(bool available) {
        leftSideCollisionCount += available ? 1 : -1;
        rollLeftAllowed.Value = leftSideCollisionCount > 0;
    }
}

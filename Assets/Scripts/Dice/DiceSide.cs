using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms;

public class DiceSide : MonoBehaviour
{
    [SerializeField]
    private AtomBaseVariable<int> diceBottomValue;
    [SerializeField]
    private AtomBaseVariable<int> diceTopValue;
    [SerializeField]
    private AtomBaseVariable<bool> isOnIce;

    [SerializeField]
    private int diceSideValue;

    private void OnTriggerEnter(Collider collider) {
        diceBottomValue.Value = diceSideValue;
        diceTopValue.Value = 7 - diceSideValue;

        isOnIce.Value = collider.tag == "Ice";
    }
}

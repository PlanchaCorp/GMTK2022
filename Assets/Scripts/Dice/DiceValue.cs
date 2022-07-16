using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms;

public class DiceValue : MonoBehaviour
{
    [SerializeField]
    private AtomBaseVariable<int> diceBottomValue;
    [SerializeField]
    private AtomBaseVariable<int> diceTopValue;

    public void OnNewSidePressed(int sideValue) {
        Debug.Log("Pressing " + sideValue);
        diceBottomValue.Value = sideValue;
        diceTopValue.Value = 7 - sideValue;
    }
}

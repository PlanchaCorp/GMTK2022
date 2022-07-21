using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms;

public class DiceSide : MonoBehaviour
{
    [SerializeField] private int diceSideValue;

    [SerializeField] private AtomBaseVariable<int> diceBottomValue;
    [SerializeField] private AtomBaseVariable<bool> isOnIce;
    [SerializeField] private AtomEvent<int> onSidePressed;

    private void OnTriggerEnter(Collider collider) {
        if (collider.tag == "Ground" || collider.tag == "Ice") {
            diceBottomValue.Value = diceSideValue;
            isOnIce.Value = collider.tag == "Ice";
            onSidePressed.Raise(diceSideValue);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms;
using UnityAtoms.BaseAtoms;

public class DiceSide : MonoBehaviour
{
    [SerializeField] private int diceSideValue;

    [SerializeField] private BoolReference isOnIce;
    [SerializeField] private AtomEvent<int> onSidePressed;

    private void OnTriggerEnter(Collider collider) {
        if (collider.tag == "Ground" || collider.tag == "Ice") {
            isOnIce.Value = collider.tag == "Ice";
            onSidePressed.Raise(diceSideValue);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms;

public class DiceSensor : MonoBehaviour
{
    [SerializeField] AtomBaseVariable<bool> sensorReachable;

    private int groundContactCount = 0;
    private int diceContactCount = 0;

    private void OnDestroy() {
        sensorReachable.Value = false;
    }

    private void OnTriggerEnter(Collider collider) {
        if (collider.tag == "Ground" || collider.tag == "Ice")
            groundContactCount++;
        else if (collider.tag == "DiceCore")
            diceContactCount++;
        sensorReachable.Value = groundContactCount > 0 && diceContactCount == 0;
    }
    private void OnTriggerExit(Collider collider) {
        if (collider.tag == "Ground" || collider.tag == "Ice")
            groundContactCount--;
        else if (collider.tag == "DiceCore")
            diceContactCount--;
        sensorReachable.Value = groundContactCount > 0 && diceContactCount == 0;
    }
}

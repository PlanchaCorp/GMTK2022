using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms;

public class GoalTile : MonoBehaviour
{
    Dictionary<string, int> colliderValues = new Dictionary<string, int>()
    {
        { "OneCollider", 1 },
        { "TwoCollider", 2 },
        { "ThreeCollider", 3 },
        { "FourCollider", 4 },
        { "FiveCollider", 5 },
        { "SixCollider", 6 },
    };

    [SerializeField]
    private AtomEvent<bool> onGoalChanged;

    [SerializeField]
    private int goalValue = 0;

    private void OnTriggerEnter(Collider collider) {
        int pressedValue = colliderValues.ContainsKey(collider.name) ? colliderValues[collider.name] : 0;
        if (pressedValue == goalValue && pressedValue != 0) {
            onGoalChanged.Raise(true);
        }
    }

    private void OnTriggerExit(Collider collider) {
        int pressedValue = colliderValues.ContainsKey(collider.name) ? colliderValues[collider.name] : 0;
        if (pressedValue == goalValue && pressedValue != 0) {
            onGoalChanged.Raise(false);
        }
    }
}

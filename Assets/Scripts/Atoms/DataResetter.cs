using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms;

public class DataResetter : MonoBehaviour
{
    [SerializeField] AtomBaseVariable[] variables;
    [SerializeField] AtomEventBase[] events;

    void Awake()
    {
    }

    void OnDestroy() {
        foreach(AtomBaseVariable variable in variables) {
            variable.Reset();
        }
        foreach(AtomEventBase atomEvent in events) {
            atomEvent.UnregisterAll();
        }
    }

}

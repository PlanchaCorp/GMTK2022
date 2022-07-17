using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms;

public class DataResetter : MonoBehaviour
{
    [SerializeField] AtomBaseVariable[] variables;

    void Awake()
    {
        foreach(AtomBaseVariable variable in variables) {
            variable.Reset();
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms;

public class CoreValueHolder : MonoBehaviour
{
    [SerializeField]
    public AtomBaseVariable<bool> isWaitingForDice;
    [SerializeField]
    public AtomBaseVariable<bool> isMoving;
}

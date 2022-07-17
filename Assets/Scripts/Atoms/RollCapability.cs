using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms;

[Serializable]
public struct RollCapability {
    [SerializeField]
    public AtomBaseVariable<int> rollTopAllowed;
    [SerializeField]
    public AtomBaseVariable<int> rollRightAllowed;
    [SerializeField]
    public AtomBaseVariable<int> rollDownAllowed;
    [SerializeField]
    public AtomBaseVariable<int> rollLeftAllowed;
    
    [SerializeField]
    public AtomEvent<Collider> onTopAvailableEvent;
    [SerializeField]
    public AtomEvent<Collider> onTopBlockedEvent;
    [SerializeField]
    public AtomEvent<Collider> onRightAvailableEvent;
    [SerializeField]
    public AtomEvent<Collider> onRightBlockedEvent;
    [SerializeField]
    public AtomEvent<Collider> onDownAvailableEvent;
    [SerializeField]
    public AtomEvent<Collider> onDownBlockedEvent;
    [SerializeField]
    public AtomEvent<Collider> onLeftAvailableEvent;
    [SerializeField]
    public AtomEvent<Collider> onLeftBlockedEvent;
}

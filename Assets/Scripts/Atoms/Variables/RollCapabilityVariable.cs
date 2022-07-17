using UnityEngine;
using System;

namespace UnityAtoms
{
    /// <summary>
    /// Variable of type `RollCapability`. Inherits from `AtomVariable&lt;RollCapability, RollCapabilityPair, RollCapabilityEvent, RollCapabilityPairEvent, RollCapabilityRollCapabilityFunction&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-lush")]
    [CreateAssetMenu(menuName = "Unity Atoms/Variables/RollCapability", fileName = "RollCapabilityVariable")]
    public sealed class RollCapabilityVariable : AtomVariable<RollCapability, RollCapabilityPair, RollCapabilityEvent, RollCapabilityPairEvent, RollCapabilityRollCapabilityFunction>
    {
        protected override bool ValueEquals(RollCapability other)
        {
            throw new NotImplementedException();
        }
    }
}

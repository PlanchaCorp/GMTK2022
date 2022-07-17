using UnityEngine;

namespace UnityAtoms
{
    /// <summary>
    /// Event of type `RollCapability`. Inherits from `AtomEvent&lt;RollCapability&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/RollCapability", fileName = "RollCapabilityEvent")]
    public sealed class RollCapabilityEvent : AtomEvent<RollCapability>
    {
    }
}

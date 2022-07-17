using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Variable Inspector of type `RollCapability`. Inherits from `AtomVariableEditor`
    /// </summary>
    [CustomEditor(typeof(RollCapabilityVariable))]
    public sealed class RollCapabilityVariableEditor : AtomVariableEditor<RollCapability, RollCapabilityPair> { }
}

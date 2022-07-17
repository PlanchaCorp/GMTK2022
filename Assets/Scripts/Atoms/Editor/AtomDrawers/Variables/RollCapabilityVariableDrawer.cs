#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Variable property drawer of type `RollCapability`. Inherits from `AtomDrawer&lt;RollCapabilityVariable&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(RollCapabilityVariable))]
    public class RollCapabilityVariableDrawer : VariableDrawer<RollCapabilityVariable> { }
}
#endif

#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `RollCapability`. Inherits from `AtomEventEditor&lt;RollCapability, RollCapabilityEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(RollCapabilityEvent))]
    public sealed class RollCapabilityEventEditor : AtomEventEditor<RollCapability, RollCapabilityEvent> { }
}
#endif

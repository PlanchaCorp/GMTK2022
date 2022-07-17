#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `RollCapability`. Inherits from `AtomDrawer&lt;RollCapabilityEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(RollCapabilityEvent))]
    public class RollCapabilityEventDrawer : AtomDrawer<RollCapabilityEvent> { }
}
#endif

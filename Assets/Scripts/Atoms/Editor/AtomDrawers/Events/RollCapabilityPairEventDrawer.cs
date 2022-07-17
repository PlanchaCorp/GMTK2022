#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `RollCapabilityPair`. Inherits from `AtomDrawer&lt;RollCapabilityPairEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(RollCapabilityPairEvent))]
    public class RollCapabilityPairEventDrawer : AtomDrawer<RollCapabilityPairEvent> { }
}
#endif

#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `RollCapabilityPair`. Inherits from `AtomEventEditor&lt;RollCapabilityPair, RollCapabilityPairEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(RollCapabilityPairEvent))]
    public sealed class RollCapabilityPairEventEditor : AtomEventEditor<RollCapabilityPair, RollCapabilityPairEvent> { }
}
#endif

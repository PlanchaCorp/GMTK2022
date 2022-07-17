using System;
using UnityEngine;
namespace UnityAtoms
{
    /// <summary>
    /// IPair of type `&lt;RollCapability&gt;`. Inherits from `IPair&lt;RollCapability&gt;`.
    /// </summary>
    [Serializable]
    public struct RollCapabilityPair : IPair<RollCapability>
    {
        public RollCapability Item1 { get => _item1; set => _item1 = value; }
        public RollCapability Item2 { get => _item2; set => _item2 = value; }

        [SerializeField]
        private RollCapability _item1;
        [SerializeField]
        private RollCapability _item2;

        public void Deconstruct(out RollCapability item1, out RollCapability item2) { item1 = Item1; item2 = Item2; }
    }
}
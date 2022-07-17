using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityAtoms;

public class TerrainRules : MonoBehaviour
{
    private const float DICE_COLLISION_DELAY = 0.2f;

    [SerializeField]
    private RollCapability[] rollCapabilities;

    private void Start() {
        foreach (RollCapability rollCapability in rollCapabilities) {
            rollCapability.onTopAvailableEvent.Register((collider) => this.UpdateMoveAvailability(collider, rollCapability.rollTopAllowed, true));
            rollCapability.onTopBlockedEvent.Register((collider) => this.UpdateMoveAvailability(collider, rollCapability.rollTopAllowed, false));
            rollCapability.onRightAvailableEvent.Register((collider) => this.UpdateMoveAvailability(collider, rollCapability.rollRightAllowed, true));
            rollCapability.onRightBlockedEvent.Register((collider) => this.UpdateMoveAvailability(collider, rollCapability.rollRightAllowed, false));
            rollCapability.onDownAvailableEvent.Register((collider) => this.UpdateMoveAvailability(collider, rollCapability.rollDownAllowed, true));
            rollCapability.onDownBlockedEvent.Register((collider) => this.UpdateMoveAvailability(collider, rollCapability.rollDownAllowed, false));
            rollCapability.onLeftAvailableEvent.Register((collider) => this.UpdateMoveAvailability(collider, rollCapability.rollLeftAllowed, true));
            rollCapability.onLeftBlockedEvent.Register((collider) => this.UpdateMoveAvailability(collider, rollCapability.rollLeftAllowed, false));
        }
    }

    private void UpdateMoveAvailability(Collider collider, AtomBaseVariable<int> rollAllowed, bool addContact) {
        if (collider.tag == "Ground" || collider.tag == "Ice") {
            rollAllowed.Value += addContact ? 1 : -1;
        } else if (collider.tag == "DiceCore") {
            rollAllowed.Value += addContact ? -1 : +1;
        }
    }

    private void OnDestroy() {
        foreach (RollCapability rollCapability in rollCapabilities) {
            rollCapability.onTopAvailableEvent.UnregisterAll();
            rollCapability.onTopBlockedEvent.UnregisterAll();
            rollCapability.onRightAvailableEvent.UnregisterAll();
            rollCapability.onRightBlockedEvent.UnregisterAll();
            rollCapability.onDownAvailableEvent.UnregisterAll();
            rollCapability.onDownBlockedEvent.UnregisterAll();
            rollCapability.onLeftAvailableEvent.UnregisterAll();
            rollCapability.onLeftBlockedEvent.UnregisterAll();
        }
    }
}

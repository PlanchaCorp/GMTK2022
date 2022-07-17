using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms;

public class HitCount : MonoBehaviour
{
    [SerializeField]
    private AtomEvent<int> onDiceMoveComplete;
    [SerializeField]
    private AtomBaseVariable<int> hitCount;

    private static float lastHitTime = 0;

    private void Start() {
        onDiceMoveComplete.Register(this.TryRegisterHit);
    }

    private void Destroy() {
        lastHitTime = 0;
    }

    private void Detroy() {
        onDiceMoveComplete.Unregister(this.TryRegisterHit);
    }

    private void TryRegisterHit() {
        if (Time.time - lastHitTime > DiceMovement.MULTI_ALLOWED_DELAY) {
            lastHitTime = Time.time;
            hitCount.Value++;
        }
    }
}

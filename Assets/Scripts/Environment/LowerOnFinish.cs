using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms;
using UniRx;

public class LowerOnFinish : MonoBehaviour
{
    [SerializeField] private AtomEvent<string> onLevelStateChange;

    [SerializeField] private AtomBaseVariable<float> loweringAmount;
    [SerializeField] private AtomBaseVariable<float> loweringSpeed;

    private bool isLowering = false;
    private float loweringProgress = 0;

    private void Awake() {
        onLevelStateChange.Observe()
            .Where(state => state == LevelStates.Completed)
            .TakeUntilDestroy(this)
            .Subscribe(_ => this.StartLowering());
    }

    public void StartLowering() {
        isLowering = true;
    }

    private void Update() {
        if (isLowering) {
            float lowerAmount = Mathf.Min(Time.deltaTime * loweringSpeed.Value, 1 - loweringProgress);
            loweringProgress += lowerAmount;
            transform.position = new Vector3(transform.position.x, transform.position.y - (lowerAmount * loweringAmount.Value), transform.position.z);

            if (loweringProgress >= 1) {
                isLowering = false;
            }
        }
    }
}

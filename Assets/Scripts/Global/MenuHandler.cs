using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityAtoms;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] private AtomBaseVariable<bool> isPauseDisplayed;
    [SerializeField] private AtomEvent<Void> togglePause;
    [SerializeField] private AtomBaseVariable<float> endLevelDelay;
    [SerializeField] private AtomEvent<Void> openEndMenu;
 
    public void TogglePauseMenu() {
        isPauseDisplayed.Value = !isPauseDisplayed.Value;
        // togglePause.Raise();
    }

    private void OnDestroy() {
        StopAllCoroutines();
    }

    public void OnGoalReached() {
        Debug.Log("Coroutine start");
        StartCoroutine("IEndLevel");
    }
 
    private IEnumerator IEndLevel() {
        yield return new WaitForSeconds(endLevelDelay.Value);
        Debug.Log("End Coroutine");
        openEndMenu.Raise();
    }
}

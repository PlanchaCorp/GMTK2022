using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityAtoms;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] private AtomBaseVariable<bool> isPauseDisplayed;
    [SerializeField] private AtomBaseVariable<float> endLevelDelay;
    [SerializeField] private AtomEvent<Void> endLevelEvent;
 
    public void TogglePauseMenu() {
        isPauseDisplayed.Value = !isPauseDisplayed.Value;
    }

    public void OnGoalReached() {
        StartCoroutine("IEndLevel");
    }
 
    private IEnumerator IEndLevel() {
        yield return new WaitForSeconds(endLevelDelay.Value);
        endLevelEvent.Raise();
        Debug.Log("Raised end level event");
    }


    public void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

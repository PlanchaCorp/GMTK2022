using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityAtoms;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] private AtomBaseVariable<bool> isPauseDisplayed;
 
    public void TogglePauseMenu() {
        isPauseDisplayed.Value = !isPauseDisplayed.Value;
    }
 
    public void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

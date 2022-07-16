using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityAtoms;

public class PauseMenuActions : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private UIDocument uiDocument;

    [SerializeField]
    private AtomEvent<Void> restart;

    private Button restartButton;    
    private Button menuButton;    
    private Button nextButton; 

    void OnEnable ()
    {
        var root = uiDocument.rootVisualElement;
        
        restartButton = root.Q<Button>("RestartButton");
        menuButton = root.Q<Button>("MenuButton");
        nextButton = root.Q<Button>("NextButton");
        restartButton.clickable.clicked += OnClickRestart;
        menuButton.clickable.clicked += OnClickMenu;
        nextButton.clickable.clicked += OnClickNext;
    }

    private void OnClickRestart() {
        Debug.Log("Restart!");
        restart.Raise();
    }
    private void OnClickMenu() {
        Debug.Log("Menu!");
        restart.Raise();
    }
    private void OnClickNext() {
        Debug.Log("Next!");
        restart.Raise();
    }
    public void onPause(bool pauseEnabled){
            uiDocument.enabled= pauseEnabled;
    }
}

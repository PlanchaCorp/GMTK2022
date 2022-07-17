using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityAtoms;


public class PauseActions : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private UIDocument uiDocument;

    [SerializeField]
    private AtomEvent<Void> restart;
    [SerializeField]
    private AtomEvent<Void> menu;
    [SerializeField]
    private AtomEvent<Void> next;
    [SerializeField] private AtomBaseVariable<bool> isPauseDisplayed;

    private Button restartButton;    
    private Button menuButton;    
    private Button nextButton; 
    private Label title;
    private VisualElement modal;

    void OnEnable(){
        var root = uiDocument.rootVisualElement;
        modal = root.Q<VisualElement>("Modal");
        restartButton = root.Q<Button>("RestartButton");
        menuButton = root.Q<Button>("MenuButton");
        nextButton = root.Q<Button>("NextButton");
        title = root.Q<Label>("Title");
        restartButton.clickable.clicked += OnClickRestart;
        menuButton.clickable.clicked += OnClickMenu;
        nextButton.clickable.clicked += OnClickNext;
    }

    private void OnDestroy() {
        restartButton.clickable.clicked -= OnClickRestart;
        menuButton.clickable.clicked -= OnClickMenu;
        nextButton.clickable.clicked += OnClickNext;
    }

    private void OnClickRestart() {
        Debug.Log("Restart!");
        restart.Raise();
    }
    private void OnClickMenu() {
        Debug.Log("Menu!");
        menu.Raise();
    }
    private void OnClickNext() {
        Debug.Log("Next!");
        next.Raise();
        
    }
    public void OnPause() {
        if (isPauseDisplayed.Value) {
            modal.style.display = DisplayStyle.Flex;
            title.text = "Pause.exe";
        } else if (modal != null && modal.style != null) {
            modal.style.display = DisplayStyle.None;
        }
    }
    public void onFinish(){
            modal.style.display = DisplayStyle.Flex;
            title.text = "Finish.exe";
    }
}

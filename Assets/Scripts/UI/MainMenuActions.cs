using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityAtoms;


public class MainMenuActions : MonoBehaviour
{
    [SerializeField]
    private UIDocument uiDocument;

    [SerializeField]
    private AtomEvent<Void> goToBegin;
    [SerializeField]
    private AtomEvent<Void> onToggleLevelSelect;

    private Button beginButton;    
    private Button levelSelectButton;   
    
    private VisualElement levelSelectGUI;

    void OnEnable() {
        var root = uiDocument.rootVisualElement;
        levelSelectGUI = root.Q<VisualElement>("LevelSelectGUI");
        beginButton = root.Q<Button>("BeginButton");
        levelSelectButton = root.Q<Button>("LevelSelectButton");
        beginButton.clickable.clicked += OnClickBegin;
        levelSelectButton.clickable.clicked += OnClickLevelSelect;
    }

    private void OnDestroy() {
        beginButton.clickable.clicked -= OnClickBegin;
        levelSelectButton.clickable.clicked -= OnClickLevelSelect;
    }

    private void OnClickBegin() {
        Debug.Log("Begin!");
        goToBegin.Raise();
    }
    private void OnClickLevelSelect() {
        Debug.Log("Select!");
        onToggleLevelSelect.Raise();
    }
    public void ToggleLevelSelect(bool enableLevelSelect) {
        if (enableLevelSelect){
            levelSelectGUI.style.display = DisplayStyle.Flex;
            // title.text = "Pause.exe";
        } else if (levelSelectGUI != null && levelSelectGUI.style != null) {
            levelSelectGUI.style.display = DisplayStyle.None;
        }
    }
}

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
    private AtomEvent<string> openFolder;
    [SerializeField]
    private SceneDispatcher sceneDispatcher;

    private Button beginButton;    
    private Button openMeadowButton;
    private Button openToundraButton;
    private Button openBeachButton;

    private VisualElement levelSelectionWindow;
    

    void OnEnable() {
        var root = uiDocument.rootVisualElement;
        beginButton = root.Q<Button>("BeginButton");
        openMeadowButton = root.Q<Button>("Meadow");
        openToundraButton = root.Q<Button>("Toundra");
        openBeachButton = root.Q<Button>("Beach");
        levelSelectionWindow =  root.Q<VisualElement>("LevelSelect");

        beginButton.clickable.clicked += OnClickBegin;
        openMeadowButton.clickable.clicked += OnClickMeadowSelect;
        openToundraButton.clickable.clicked += OnClickToundraSelect;
        openBeachButton.clickable.clicked += OnClickBeachSelect;
        
    }

    private void OnDisable() {
        beginButton.clickable.clicked -= OnClickBegin;
        openMeadowButton.clickable.clicked -= OnClickMeadowSelect;
        openToundraButton.clickable.clicked -= OnClickToundraSelect;
        openBeachButton.clickable.clicked -= OnClickBeachSelect;
    }

    private void OnClickBegin() {
        sceneDispatcher.LoadLevelWithIndex(0,0);
    }
    private void OnClickMeadowSelect() {
        openFolder.Raise("Meadow");
    }
    private void OnClickToundraSelect() {
        openFolder.Raise("Toundra");
    }
    private void OnClickBeachSelect() {
        openFolder.Raise("Beach");
    }

}

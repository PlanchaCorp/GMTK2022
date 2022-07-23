using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityAtoms;
using UniRx;
using UnityAtoms.BaseAtoms;
using UnityEngine.InputSystem.Controls;

public class LevelMenuAction : MonoBehaviour
{
    [SerializeField]
    private UIDocument uiDocument;
    [SerializeField]
    private AtomEvent<string> openFolder;
    [SerializeField]
    private SceneDispatcher sceneDispatcher;

    private string folder;

    private VisualElement levelSelectionWindow;
    private Label title;
    private Button closeButton;
    private int worldIndex;
    

    private void Awake(){
        openFolder.Observe()
            .TakeUntilDestroy(this)
            .Subscribe(s => OpenWindow(s));
    }
    void OnEnable() {
        var root = uiDocument.rootVisualElement;
        levelSelectionWindow =  root.Q<VisualElement>("LevelSelect");
        title =  root.Q<Label>("Title");
        closeButton = root.Q<Button>("WindowsButton");
        levelSelectionWindow.Query<Button>(className: "levelFile").ForEach((button) => {
            button.RegisterCallback<ClickEvent>((evt) => onClickButton(button.viewDataKey));
        });
        closeButton.clickable.clicked += onCloseClick;
    }

    private void onClickButton(string levelIndex){
        sceneDispatcher.LoadLevelWithIndex(worldIndex,int.Parse(levelIndex));
    }

    private void onCloseClick(){
        levelSelectionWindow.style.display = DisplayStyle.None;
    }
    private void OpenWindow(string folder){
        levelSelectionWindow.style.display = DisplayStyle.Flex;
        this.folder = folder;
        title.text = this.folder;
        if(folder == "Meadow"){
            worldIndex = 0;
        } else if(folder == "Toundra"){
            worldIndex = 1;
        } else {
            worldIndex = 2;
        }
    }
}

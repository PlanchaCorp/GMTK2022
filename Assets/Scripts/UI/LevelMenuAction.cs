using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityAtoms;

public class LevelMenuAction : MonoBehaviour
{
    [SerializeField]
    private UIDocument uiDocument;

    [SerializeField]
    private AtomEvent<string> loadLevel;

    private string folder;

    private VisualElement levelSelectionWindow;
    private Label title;
    private Button button1;
    private Button button2;
    private Button button3;
    private Button button4;
    private Button button5;
    private Button closeButton;
    private LevelManager levelManager;
    private string[] levelToLoad;
    

    void OnEnable() {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();

        var root = uiDocument.rootVisualElement;
        levelSelectionWindow =  root.Q<VisualElement>("LevelSelect");
        title =  root.Q<Label>("Title");
        button1 = root.Q<Button>("Button1");
        button2 = root.Q<Button>("Button2");
        button3 = root.Q<Button>("Button3");
        button4 = root.Q<Button>("Button4");
        button5 = root.Q<Button>("Button5");

        button1.clickable.clicked += onClickButton1;
        button2.clickable.clicked += onClickButton2;
        button3.clickable.clicked += onClickButton3;
        button4.clickable.clicked += onClickButton4;
        button5.clickable.clicked += onClickButton5;
        closeButton.clickable.clicked += onCloseClick;
    }

    private void onClickButton1(){
        loadLevel.Raise(levelToLoad[0]);
    }
        private void onClickButton2(){
        loadLevel.Raise(levelToLoad[1]);
    }

    private void onClickButton3(){
        loadLevel.Raise(levelToLoad[2]);
    }

    private void onClickButton4(){
        loadLevel.Raise(levelToLoad[3]);
    }

    private void onClickButton5(){
        loadLevel.Raise(levelToLoad[4]);
    }


    public void onCloseClick(){
        levelSelectionWindow.style.display = DisplayStyle.None;
    }
    public void OpenWindow(string folder){
        levelSelectionWindow.style.display = DisplayStyle.Flex;
        this.folder = folder;
        title.text = this.folder;
        if(folder == "Meadow"){
            this.levelToLoad = levelManager.meadowLevels;
        } else if(folder == "Toundra"){
            this.levelToLoad = levelManager.tundraLevels;
        } else {
            this.levelToLoad = levelManager.beachLevels;
        }
        }  
}

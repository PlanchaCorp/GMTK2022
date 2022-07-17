using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityAtoms;

public class InGameActions : MonoBehaviour
{
    [SerializeField]
    private UIDocument uiDocument;

    [SerializeField]
    private AtomEvent<Void> restart;
    [SerializeField]
    private AtomBaseVariable<bool> isPausedDisplayed;
    

    private Button pauseButton;
    private Button restartButton;
    private Label moveCountLabel;

    private void OnEnable()
    {
        var root = uiDocument.rootVisualElement;
        pauseButton = root.Q<Button>("Pause");
        restartButton = root.Q<Button>("Restart");
        moveCountLabel = root.Q<Label>("MoveCountLabel");

        pauseButton.clickable.clicked += OnClickPause;
        restartButton.clickable.clicked += OnClickRestart;
    }

    private void OnDestroy() {
        pauseButton.clickable.clicked -= OnClickPause;
        restartButton.clickable.clicked -= OnClickRestart;
    }

    private void OnClickRestart() {
        Debug.Log("Restart!");
        restart.Raise();
    }
    private void OnClickPause() {
        Debug.Log("Pause!");
        isPausedDisplayed.Value = !isPausedDisplayed.Value;
    }

    public void displayMovesCount(int count){
        moveCountLabel.text= count + " Moves";
    }

}

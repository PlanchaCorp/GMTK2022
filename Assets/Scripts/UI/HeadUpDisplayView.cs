using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityAtoms;
using UnityAtoms.FSM;
using UnityAtoms.BaseAtoms;
using UniRx;

public class HeadUpDisplayView : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    [SerializeField] private IntVariable moveCount;

    [SerializeField] private AtomEvent<Void> onRestartRequest;
    [SerializeField] private AtomEvent<Void> onPauseRequest;
    
    private Button pauseButton;
    private Button restartButton;
    private Label moveCountLabel;

    private void Awake(){
        moveCount.ObserveChange()
            .TakeUntilDestroy(this)
            .Subscribe(displayMovesCount);
    }
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

    public  void OnClickRestart() {
        onRestartRequest.Raise();
    }
    public void OnClickPause() {
        onPauseRequest.Raise();
    }

    private void displayMovesCount(int count){
        moveCountLabel.text = count + " Moves";
    }

}

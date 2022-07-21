using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityAtoms;
using UnityAtoms.FSM;
using UnityAtoms.BaseAtoms;
using UniRx;

public class InGameActions : MonoBehaviour
{
    [SerializeField]
    private UIDocument uiDocument;
    [SerializeField]
    private IntVariable moveCount;

    [SerializeField]
    private AtomEvent<Void> restart;
    [SerializeField] private FiniteStateMachineReference levelState;
    

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

    private void OnClickRestart() {
        restart.Raise();
    }
    private void OnClickPause() {
       if (levelState.Machine.Value == LevelStates.InProgress){
            levelState.Machine.Dispatch(LevelTransition.Pause);
            GetComponent<PauseActions>().OnPause();
    }
        else {
            levelState.Machine.Dispatch(LevelTransition.Unpause);
            GetComponent<PauseActions>().ClosePause();
        }
    }

    private void displayMovesCount(int count){
        moveCountLabel.text= count + " Moves";
    }

}

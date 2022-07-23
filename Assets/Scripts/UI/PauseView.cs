using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityAtoms;
using UniRx;

public class PauseView : MonoBehaviour
{
    private readonly float FINISH_POPUP_DELAY = 0.7f;

    [SerializeField] private UIDocument uiDocument;

    [SerializeField] private AtomEvent<Void> onRestartRequest;
    [SerializeField] private AtomEvent<Void> onMainMenuRequest;
    [SerializeField] private AtomEvent<Void> onNextLevelRequest;

    [SerializeField] private AtomEvent<string> onLevelStateChanged;

    private Button restartButton;    
    private Button menuButton;    
    private Button nextButton; 
    private Label title;
    private VisualElement modal;


    void Awake(){
        onLevelStateChanged.Observe()
            .Where(state => state == LevelStates.Paused)
            .TakeUntilDestroy(this)
            .Subscribe(_ => OnPause());
        onLevelStateChanged.Observe()
            .Where(state => state == LevelStates.InProgress)
            .TakeUntilDestroy(this)
            .Subscribe(_ => ClosePause());
        onLevelStateChanged.Observe()
            .Where(state => state == LevelStates.Completed)
            .TakeUntilDestroy(this)
            .Subscribe(_ => StartCoroutine(OnFinish()));
    }
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
        onRestartRequest.Raise();
    }
    private void OnClickMenu() {
        onMainMenuRequest.Raise();
    }
    private void OnClickNext() {
        onNextLevelRequest.Raise();
    }

    private void OnPause() {
        modal.style.display = DisplayStyle.Flex;
        title.text = "Pause.exe";
    }
    private void ClosePause(){
        modal.style.display = DisplayStyle.None;
    }
    private IEnumerator OnFinish() {
        yield return new WaitForSeconds(FINISH_POPUP_DELAY);
        modal.style.display = DisplayStyle.Flex;
        title.text = "Finish.exe";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityAtoms;

public class InGameGUIActions : MonoBehaviour
{     
    [SerializeField]
    private UIDocument uiDocument;

    [SerializeField]
    private AtomEvent<Void> restart;

    private Button restartButton;    

    void OnEnable ()
    {
        var root = uiDocument.rootVisualElement;
        
        restartButton = root.Q<Button>("RestartButton");
        restartButton.clickable.clicked += OnRestart;
    }

    private void OnRestart() {
        Debug.Log("Restart!");
        restart.Raise();
    }
}

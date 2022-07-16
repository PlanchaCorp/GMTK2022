using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InGameGUIActions : MonoBehaviour
{     
    [SerializeField]
    private UIDocument uiDocument;

    private Button restartButton;    

    void OnEnable ()
    {
        var root = uiDocument.rootVisualElement;
        
        restartButton = root.Q<Button>("RestartButton");
        restartButton.clickable.clicked += OnRestart;
    }

    private void OnRestart() {
        Debug.Log("Restart!");
    }
}

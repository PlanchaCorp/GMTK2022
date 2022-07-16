using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private UnityAtoms.AtomBaseVariable<Vector2> playerMovement;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.Value.magnitude > 0) {
            Debug.Log(playerMovement.Value);
        }
    }
}

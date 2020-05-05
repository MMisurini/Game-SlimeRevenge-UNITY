using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerScene : MonoBehaviour
{
    [SerializeField] private bool isPortal = false;
    void Start() {
        DontDestroyOnLoad(this);
    }

    void Update(){
        if (isPortal) {
            transform.position = GameObject.FindGameObjectWithTag("Spawn").transform.position;
            GetComponent<CharacterController>().enabled = true;
            isPortal = false;
        }
    }

}

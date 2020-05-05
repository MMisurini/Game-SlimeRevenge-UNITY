using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerPush : MonoBehaviour
{
    private GameObject objectPush = null;
    private bool isPush = false;
    [SerializeField]private bool isAbove = false;
    [SerializeField] private float radiusDetected = 1f;

    private ControllerMatriz cMatriz = null;

    void Start() {
        cMatriz = GetComponent<ControllerMatriz>();
    }

    void Update(){
        if (cMatriz.Attack.push == pushNives.Basico) {
            objectPush = cMatriz.Collision.Area(radiusDetected);

            if (objectPush != null) {
                cMatriz.Text.EnableTutorial(0,3);
                if (isPush) {
                    CheckInputGetOutPush(false);

                    Vector3 lookVector = new Vector3(objectPush.transform.position.x, 0, objectPush.transform.position.z) - transform.position;
                    transform.localRotation = Quaternion.LookRotation(lookVector);

                    cMatriz.cMoviment.DisableMouseAndKeybord = false;
                    cMatriz.Animation.SetPush(true);
                } else {
                    cMatriz.cMoviment.DisableMouseAndKeybord = true;
                    cMatriz.Animation.SetPush(false);

                    CheckInputGetOutPush(true);
                    cMatriz.Animation.SetWalkPush(0, 1);
                }
            } else {
                isPush = false;

                cMatriz.cMoviment.DisableMouseAndKeybord = true;
                cMatriz.Animation.SetPush(false);

                CheckInputGetOutPush(true);
                cMatriz.Animation.SetWalkPush(0, 1);
            }
        }
    }

    void CheckInputGetOutPush(bool value) {
        if (Input.GetKeyDown(KeyCode.F)) 
            if(!isAbove)
                isPush = value;    
    }

    public bool Above {
        get { return isAbove; }
        set { isAbove = value; }
    }

    public bool Enable {
        get { return isPush; }
    }

}

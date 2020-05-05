using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [Header("Distance Player")]
    public Vector3 offSetPos = Vector3.zero;

    private Camera cam;
    private ControllerMatriz player = null;

    [SerializeField] private bool isGame = false;

    private Animation anim = null;

    public void MenuToGame(bool value){
        isGame = value;

        if(cam == null)
            cam = Camera.main;

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<ControllerMatriz>();

        if(anim == null)
            anim = Master.main.Canvas.GetComponent<Animation>();

        if (isGame) {
            GameIn();
        } else {
            GameOut();
        }
    }

    void GameIn() {
        anim.Play("MenuOut");

        player.Moviment = true;
        player.cMoviment.DisableMouseAndKeybord = true;

        if (cam.GetComponent<CameraAnimation>().enabled)
            cam.GetComponent<CameraClamp>().enabled = false;

        if (!cam.GetComponent<CameraClamp>().enabled)
            cam.GetComponent<CameraClamp>().enabled = true;
    }

    void GameOut() {
        if (!cam.GetComponent<CameraAnimation>().enabled)
            cam.GetComponent<CameraClamp>().enabled = true;

        if (cam.GetComponent<CameraClamp>().enabled)
            cam.GetComponent<CameraClamp>().enabled = false;
    }

    private void OnApplicationQuit() {
        isGame = false;
    }

    public void Pause (){
        if (Input.GetKeyDown(KeyCode.Escape)) {

        }
    }

    public bool Game {
        get { return isGame; }
    }
}

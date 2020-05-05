using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ControllerText : MonoBehaviour
{
    public TextInput text_tutorial;
    [SerializeField] private Scenes[] scenes;


    void OnEnable(){
        if(text_tutorial == null)
            text_tutorial = Master.main.TextToTutorial;
    }

    ScenesActive CheckScene() {
        Scene scene = SceneManager.GetActiveScene();
        foreach (Scenes a in scenes) {
            if(a.scenesActive.ToString() == scene.name) {
                return a.scenesActive;
            }
        }

        return ScenesActive.Tutorial;
    }

    public void EnableTutorial(int index, float timeOut) {
        text_tutorial.gameObject.SetActive(true);

        CheckText(index);

        text_tutorial.GetComponent<DisableAfterTime>().timeAfterDisable = timeOut;
    }
    public void DisableTutorial() {
        text_tutorial.GetComponent<DisableAfterTime>().distanceAfterDisable = false;
    }

    void CheckText(int index) {
        text_tutorial.InputTextWithTimeDisable(SceneActive.text_tutorial[index], 3f, true);
    }

    public Scenes SceneActive {
        get {
            Scene scene = SceneManager.GetActiveScene();
            foreach (Scenes a in scenes) {
                if (a.scenesActive.ToString() == scene.name) {
                    return a;
                }
            }
            return scenes[0];
        }
    }

    [Serializable]
    public struct Scenes {
        public ScenesActive scenesActive;
        public string[] text_tutorial;
    }
}

public enum ScenesActive { Tutorial, Forest, Fire, Cave, Snow, Desert, Sky}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using TMPro;

public class Master : MonoBehaviour
{
    [SerializeField] private Menu menu = null;
    [SerializeField] private DayAndNight date = null;

    [SerializeField] private GameObject videoToTutorial = null;
    [SerializeField] private AudioSource soundTalk = null;

    [SerializeField] private GameObject canvas = null;

    [SerializeField] private TextInput textToTutorial = null;

    public static Master main { get; set; }
    private void Awake() {
        main = this;

        menu = GetComponent<Menu>();
        date = GetComponent<DayAndNight>();

        canvas = transform.GetChild(0).gameObject;

        videoToTutorial = GameObject.FindGameObjectWithTag("Video").transform.GetChild(0).gameObject;
        soundTalk = transform.GetChild(1).GetComponent<AudioSource>();

        textToTutorial = transform.GetChild(0).GetChild(1).GetChild(1).GetChild(0).GetComponent<TextInput>();
    }

    public Menu Menu {
        get { return menu; }
    }

    public DayAndNight DayAndNight {
        get { return date; }
    }

    public AudioSource SoundToTalk {
        get { return soundTalk; }
    }

    public GameObject VideoToTutorial {
        get { return videoToTutorial; }
    }
    public TextInput TextToTutorial {
        get { return textToTutorial; }
    }
    public GameObject Canvas {
        get { return canvas; }
    }
}

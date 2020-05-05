using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class OldmanTeacher : MonoBehaviour
{
    private GameObject videoPrefab;
    [SerializeField] private Teach teachToPlayer = Teach.Null;
    [Space(10)]
    [SerializeField] private string subtitleToTalk = null;
    [SerializeField] private AudioClip talkThisTeacher = null;

    private VideoPlayer video = null;
    private TextInput textInput = null;
    private bool isTalk = false;

    void Start() {
        videoPrefab = Master.main.VideoToTutorial;

        textInput = Master.main.TextToTutorial;

        if(videoPrefab.transform.parent.gameObject.activeInHierarchy)
            videoPrefab.transform.parent.gameObject.SetActive(false);
    }

    void Update() {
        if (isTalk) {
            if (subtitleToTalk != "") {
                textInput.InputTextWithTimeDisable(subtitleToTalk, talkThisTeacher.length);

                if (Master.main.SoundToTalk.clip == null) {
                    Master.main.SoundToTalk.clip = talkThisTeacher;
                    Master.main.SoundToTalk.Play();
                }

                StartCoroutine(DelayAudio(talkThisTeacher.length));
            } else {
                videoPrefab.transform.parent.gameObject.SetActive(true);

                StartCoroutine(DelayVideoToTeach(ChooseVideo(video)));
                isTalk = false;
            }
        }
    }

    IEnumerator DelayAudio(float value) {
        yield return new WaitForSeconds(value);
        subtitleToTalk = "";
    }

    public void VideoTutorial() {
        video = videoPrefab.GetComponent<VideoPlayer>();

        isTalk = true;
    }
    IEnumerator DelayVideoToTeach(float value) {
        yield return new WaitForSeconds(value);

        switch (teachToPlayer) {
            case Teach.Push:
                GameObject.FindGameObjectWithTag("Player").GetComponent<ControllerMatriz>().Attack.push = pushNives.Basico;
                Close();
                break;
            case Teach.Throw:
                GameObject.FindGameObjectWithTag("Player").GetComponent<ControllerMatriz>().Attack.Throw.niveis = throwNiveis.Basico;
                Close();
                break;
            case Teach.JumpAttack:
                GameObject.FindGameObjectWithTag("Player").GetComponent<ControllerMatriz>().Attack.JumpAttack.niveis = jumpAttackNiveis.Basico;
                Close();
                break;
            case Teach.Dash:
                GameObject.FindGameObjectWithTag("Player").GetComponent<ControllerMatriz>().Attack.Dash.niveis = dashNiveis.Basico;
                Close();
                break;
            case Teach.Melee:
                Close();
                break;
        }
    }

    void Close() {
        GameObject.FindGameObjectWithTag("Player").GetComponent<ControllerMatriz>().cMoviment.DisableMouseAndKeybord = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<ControllerMatriz>().Moviment = true;

        videoPrefab.GetComponent<VideoPlayer>().clip = null;

        if (videoPrefab.transform.parent.gameObject.activeInHierarchy)
            videoPrefab.transform.parent.gameObject.SetActive(false);
    }

    float ChooseVideo(VideoPlayer video) {
        switch (teachToPlayer) {
            case Teach.Push:
                var c = Resources.Load<VideoClip>("Video/push");
                video.clip = c;

                return float.Parse(video.length.ToString());
            case Teach.Throw:
                var b = Resources.Load<VideoClip>("Video/throw");
                video.clip = b;

                return float.Parse(video.length.ToString());
            case Teach.JumpAttack:
                var a = Resources.Load<VideoClip>("Video/jumpattack");
                video.clip = a;

                return float.Parse(video.length.ToString());
            case Teach.Dash:
                var d = Resources.Load<VideoClip>("Video/dash");
                video.clip = d;

                return float.Parse(video.length.ToString());
            case Teach.Melee:
                return float.Parse(video.length.ToString());
        }

        return 0;
    }

    public enum Teach {Null, Push, Throw, JumpAttack, Dash, Melee}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisableAfterTime : MonoBehaviour
{
    private TextMeshProUGUI tutorial_txt;
    public float timeAfterDisable = 0f;
    public bool distanceAfterDisable = false;
    private float timer = 0f;

    private void OnEnable() {
        tutorial_txt = GetComponent<TextMeshProUGUI>();
    }

    void Update(){
        if (gameObject.activeInHierarchy && tutorial_txt.text != "" && timeAfterDisable != 0) {
            timer += Time.deltaTime;
            if (timer > timeAfterDisable && !distanceAfterDisable) {
                gameObject.SetActive(false);
                timeAfterDisable = 0f;
            }
        }
    }

}

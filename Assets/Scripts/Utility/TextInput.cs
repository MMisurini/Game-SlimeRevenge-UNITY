using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextInput : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textToInput;
    [SerializeField] private DisableAfterTime disableAftetTime = null;

    // Update is called once per frame
    public void InputTextWithTimeDisable(string value, float time){
        textToInput = GetComponent<TextMeshProUGUI>();
        disableAftetTime = GetComponent<DisableAfterTime>();

        textToInput.text = value;
        disableAftetTime.timeAfterDisable = time;
    }

    public void InputTextWithTimeDisable(string value, float time, bool nearObject) {
        textToInput = GetComponent<TextMeshProUGUI>();
        disableAftetTime = GetComponent<DisableAfterTime>();

        textToInput.text = value;
        disableAftetTime.timeAfterDisable = time;
        disableAftetTime.distanceAfterDisable = nearObject;
    }
}

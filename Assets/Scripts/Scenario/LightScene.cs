using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScene : MonoBehaviour
{
    [SerializeField]private Light sun = null;

    private Light streetLight = null;

    private void OnEnable() {
        streetLight = transform.GetChild(0).GetChild(0).GetComponent<Light>();
    }

    void Update(){
        if(sun == null)
            sun = GameObject.FindGameObjectWithTag("Sun").GetComponent<Light>();

        if (CheckPositionSun()) {
            streetLight.enabled = true;
        } else {
            streetLight.enabled = false;
        }
    }

    bool CheckPositionSun() {
        if (sun.intensity == 0) {
            return true;
        }

        return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private ChooseLoadScene cls = null;

    private DayAndNight dn = null;
    private Menu mn = null;

    void Start() {
        cls = ChooseLoadScene.Instance;

        dn = GetComponent<DayAndNight>();
        mn = GetComponent<Menu>();
    }

    void Update(){
        if (cls.GetSceneAtual() != cls.GetSceneIndex(0)) {
            dn.Date();
            mn.Pause();
        }
    }
}

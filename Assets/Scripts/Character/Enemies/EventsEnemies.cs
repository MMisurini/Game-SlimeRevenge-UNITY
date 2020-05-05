using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsEnemies : MonoBehaviour
{
    private ControllerEnemies cEnemies;
    public void Attack() {
        cEnemies = GetComponent<ControllerEnemies>();

        switch (cEnemies.TypeEnemies) {
            case TypeEnemies.Simple:
                cEnemies.Animator.SetInteger("do_attack", 0);
                break;
            case TypeEnemies.Strong:
                cEnemies.Animator.SetInteger("do_attack", 0);
                break;
        }
    }
}

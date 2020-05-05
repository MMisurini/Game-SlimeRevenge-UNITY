using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerDieWater : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.name == "Water") {
            GetComponent<ControllerEnemies>().Hit(500, Vector3.zero, 0);
        }
    }
}

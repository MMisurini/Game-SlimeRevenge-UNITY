using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveCollision : MonoBehaviour
{
    private bool triggerEnter = false;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            other.GetComponent<ControllerText>().EnableTutorial(1,3);
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.tag == "Player") {
            triggerEnter = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            triggerEnter = false;
        }
    }
}

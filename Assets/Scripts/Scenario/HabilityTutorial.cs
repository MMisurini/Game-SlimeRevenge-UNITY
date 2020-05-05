using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabilityTutorial : MonoBehaviour
{
    [SerializeField] private int index = 0;
    [SerializeField] private float timeOutMessage = 2f;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            other.GetComponent<ControllerText>().EnableTutorial(index, timeOutMessage);

            Destroy(gameObject);
        }
    }
}

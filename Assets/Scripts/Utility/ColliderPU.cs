using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderPU : MonoBehaviour
{
    [SerializeField] private float valueGosma = 0f;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            other.GetComponent<ControllerHealth>().Life += valueGosma;

            Destroy(gameObject);
        }
    }
}

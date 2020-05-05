using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    private ControllerMatriz cMatriz = null;

    void Start() {
        cMatriz = GameObject.FindGameObjectWithTag("Player").GetComponent<ControllerMatriz>();    
    }

    private void OnTriggerEnter(Collider other) {
        cMatriz.Animation.SetAttack(0);
        if (other.tag == "Enemies") {
            other.GetComponent<ControllerEnemies>().Hit(cMatriz.Attack.Damage, cMatriz.Attack.transform.TransformDirection(Vector3.forward), cMatriz.Attack.ImpactForce);
        }
    }
}

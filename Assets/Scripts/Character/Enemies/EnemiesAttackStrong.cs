using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesAttackStrong : MonoBehaviour
{
    [SerializeField] private float impactForce = 0.5f;
    [SerializeField] private float damage = 15f;

    [SerializeField] private Transform enemiesStrong = null;

    void OnTriggerEnter(Collider hit) {
        if (hit.GetComponent<Collider>().tag == "Player") {
            hit.GetComponent<Collider>().GetComponent<ControllerMove>().AddImpact(enemiesStrong.TransformDirection(Vector3.forward), impactForce);
            hit.GetComponent<Collider>().GetComponent<ControllerHealth>().DamageOnPlayer(damage);
        }
    }
}

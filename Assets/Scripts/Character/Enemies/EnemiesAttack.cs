using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesAttack : MonoBehaviour
{
    private Vector3 playerPosition = Vector3.zero;
    private CharacterController cController = null;

    [SerializeField] private float speed = 0.5f;
    [SerializeField] private float impactForce = 0.5f;
    [SerializeField] private float damage = 15f;

    void OnEnable() {
        cController = GetComponent<CharacterController>();    
    }

    void FixedUpdate() {
            cController.Move(playerPosition * speed * Time.deltaTime);

            Destroy(gameObject, 3);
    }

    void OnControllerColliderHit(ControllerColliderHit hit) {
        if (hit.collider.tag == "Player") {
            hit.collider.GetComponent<ControllerMove>().AddImpact(playerPosition, impactForce);
            hit.collider.GetComponent<ControllerHealth>().DamageOnPlayer(damage);
            Destroy(gameObject);
        }  
    }

    public Vector3 PlayerPosition {
        set { playerPosition = value; }
    }
}

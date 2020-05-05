using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionThrow : MonoBehaviour
{
    private Vector3 enemiesPosition = Vector3.zero;
    [Header("UI")]
    [SerializeField] private Sprite obj = null;
    [Header("Atributos")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float impactForce = 5f;
    [SerializeField] private float damage = 15f;

    public bool isThrow = false;

    void FixedUpdate(){
        if (isThrow) {
            transform.position += enemiesPosition * Time.fixedDeltaTime * speed;

            Destroy(gameObject, 3);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Enemies") {
            other.GetComponent<ControllerEnemies>().Hit(damage, enemiesPosition, impactForce);
            Destroy(gameObject);
        }
    }

    public Vector3 EnemiesPosition {
        get { return enemiesPosition; }
        set { enemiesPosition = value; }
    }

}

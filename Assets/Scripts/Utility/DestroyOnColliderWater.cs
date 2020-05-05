using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnColliderWater : MonoBehaviour
{
    private bool destroy = false;
    void FixedUpdate(){
        if (destroy)
            Destroy(gameObject, 4);
    }

    void OnCollisionEnter(UnityEngine.Collision collision) {
        if (collision.collider.name == "Water") {
            GetComponent<BoxCollider>().isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.name == "Water") {
            destroy = true;
            
            StartCoroutine(SetGravity(.1f));

            GetComponent<BoxCollider>().isTrigger = true;
        }
    }

    IEnumerator SetGravity(float value) {
        yield return new WaitForSeconds(value);
        GetComponent<Rigidbody>().drag = 15;
    }
}

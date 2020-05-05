using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private Vector2 rotate = Vector2.zero;
    [SerializeField] private Vector3 rotateEixo = Vector2.zero;

    void FixedUpdate(){
        if(rotate != Vector2.zero)
            transform.Rotate(new Vector3(Random.Range(rotate.x, rotate.y), Random.Range(rotate.x, rotate.y), Random.Range(rotate.x, rotate.y)) * Time.deltaTime);

        if(rotateEixo != Vector3.zero)
            transform.Rotate(rotateEixo * Time.deltaTime);
    }
}

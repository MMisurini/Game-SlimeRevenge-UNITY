using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerDestroyFade : MonoBehaviour
{
    [SerializeField] private float speedDestroy = 1f;
    [SerializeField] private float timeDestroyInSecond = 1f;
    void Update(){

        transform.localScale -= new Vector3(1f,1f,1f) * Time.deltaTime * speedDestroy;
        Destroy(gameObject, timeDestroyInSecond);
    }
}

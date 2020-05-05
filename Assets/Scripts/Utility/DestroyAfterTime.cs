using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float timeToDestroy = 3f;
    void Update(){
        Destroy(gameObject, timeToDestroy);
    }
}

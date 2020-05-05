using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPushIsKnimatic : MonoBehaviour
{
    [SerializeField] private float maxRadius = 2f;
    private void Update() {
        Collider[] overlaps = new Collider[1];

        for (int i = 0; i < overlaps.Length; i++) {
            overlaps[i] = null;
        }

        int layerMask = 1 << 10;

        int count = Physics.OverlapSphereNonAlloc(transform.position, maxRadius, overlaps, layerMask);

        for (int i = 0; i < count; i++) {
            if (overlaps[i] == null) {
                GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }
}

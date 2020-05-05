using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerGosma : MonoBehaviour
{
    [SerializeField] private GameObject gosma_prefab = null;
    [SerializeField] private float currentTimeGosma = 1f;

    private ControllerMatriz cMatriz = null;

    private bool isCreateGosma = true;
    public bool isWalk = false;

    void Start() {
        cMatriz = GetComponent<ControllerMatriz>();    
    }

    void Update() {
        if (isWalk && !cMatriz.Push.Enable) {
            if (isCreateGosma) {
                if (cMatriz.cMoviment.CharController.isGrounded) {
                    GameObject a = Instantiate(gosma_prefab, transform.position, Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)));
                    a.transform.SetParent(GameObject.FindGameObjectWithTag("Scenarios").transform);
                }

                StartCoroutine(CreateGosma(currentTimeGosma));

                isCreateGosma = false;
            }
        }
    }

    IEnumerator CreateGosma(float time) {
        yield return new WaitForSeconds(time);
        isCreateGosma = true;
    }
}

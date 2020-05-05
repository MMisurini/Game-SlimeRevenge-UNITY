using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Portal : MonoBehaviour
{
    public string menssagemToCollision = null;
    private TextMeshProUGUI canvasText = null;

    // Start is called before the first frame update
    void OnEnable() {
        canvasText = GameObject.Find("Scenario TXT").GetComponent<TextMeshProUGUI>();
    }

    private void OnCollisionEnter(UnityEngine.Collision coll) {
        if(coll.collider.tag == "Player") {
            if (coll.collider.GetComponent<ControllerMatriz>().Itens.keys == 2) {
                coll.collider.GetComponent<ControllerMatriz>().Itens.keys -= 2;
                gameObject.SetActive(false);
            } else {
                canvasText.text = menssagemToCollision;
            }
        }
    }
}

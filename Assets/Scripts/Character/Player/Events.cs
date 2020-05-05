using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{
    private ControllerMatriz matriz = null;
    private 

    void Start() {
        matriz = GameObject.FindGameObjectWithTag("Player").GetComponent<ControllerMatriz>();
    }

    public void JumpEvent() {
        matriz.Animation.SetJump(false);
    }
    public void Attack1Event() {
        matriz.Animation.SetAttack(0);
    }
    public void JumpAttackEvent() {
        matriz.Moviment = true;
        matriz.cMoviment.DisableMouseAndKeybord = true;
    }
    public void ThrowEvents() {
        GameObject a = Instantiate(matriz.Attack.Throw.objectAttack, matriz.Attack.Throw.target.position, matriz.Attack.Throw.objectAttack.transform.rotation);
        a.GetComponent<CollisionThrow>().EnemiesPosition = matriz.transform.TransformDirection(Vector3.forward);
        a.GetComponent<CollisionThrow>().isThrow = true;

        matriz.Attack.Throw.objectAttack = null;
        matriz.Animation.SetThrow(0);
    }

}

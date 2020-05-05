using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerAnimation : MonoBehaviour
{
    private Animator anim = null;

    private float timer = 0;

    void Start(){
        anim = transform.GetChild(0).GetChild(0).GetComponent<Animator>();
    }

    
    /* VOID */

    public void SetJump(bool value) {
        anim.SetBool("do_jump",value);
    }

    public void SetDeath(int value) {
        anim.SetInteger("do_death", value);
    }
    public void SetThrow(int value) {
        anim.SetInteger("do_throw", value);
    }
    public float GetAnimationLenght() {
        return CurrentStateInfo(0).length;
    }

    public void SetCrouch(int value) {
        anim.SetInteger("do_crouch", value);
    }

    public void SetWalk(int value, float speed) {
        if (CurrentStateInfo(0).IsName("Walk"))
            anim.speed = speed + 0.6f;

        anim.SetInteger("do_walk", value);
    }

    public void SetWalkCrouch(int value, float speed) {
        if (CurrentStateInfo(0).IsName("Walk Crouch"))
            anim.speed = speed;

        anim.SetInteger("do_walkcrounch", value);
    }

    public void SetWalkBack(int value, float speed) {
        if (CurrentStateInfo(0).IsName("WalkBack"))
            anim.speed = speed;

        anim.SetInteger("do_walkback", value);
    }

    public void SetAttack(int value) {
        anim.SetInteger("do_attack", value);
    }

    public void SetjumpAttack(int value) {
        anim.SetInteger("do_jumpattack", value);
    }
    public void SetPush(bool value) {
        anim.SetBool("do_push", value);
    }
    public void SetWalkPush(int value, float speed) {
        if (CurrentStateInfo(0).IsName("WalkPush"))
            anim.speed = speed;

        anim.SetInteger("do_pushwalk", value);
    }
    public int GetAttack() {
        return anim.GetInteger("do_attack");
    }

    public int GetCrounch() {
        return anim.GetInteger("do_crouch");
    }

    public void SetIdle() {
        anim.SetInteger("do_idle",Random.Range(0,3));
    }

    public void ResetIdle() {
        if (!CurrentStateInfo(0).IsName("Idle Static"))
            anim.SetInteger("do_idle", 0);
    }

    /* RETURN */
    public AnimatorStateInfo CurrentStateInfo(int value) {
        return anim.GetCurrentAnimatorStateInfo(value);
    }

    public bool Delay(float delay) {
        timer += Time.deltaTime;
        if (timer > delay) {
            timer = 0;

            if (!CurrentStateInfo(0).IsName("Walk") && Keys.DontMove())
                return true;
        }

        return false;
    }
}

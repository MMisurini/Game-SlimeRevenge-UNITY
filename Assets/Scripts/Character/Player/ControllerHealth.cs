using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerHealth : MonoBehaviour
{
    [Header("Amount Life")]
    [SerializeField] private float life = 100f;
    [SerializeField] private float limitLife = 100f;
    [SerializeField] private float delayLostLife = 3f;

    private float valueUseLife = 5f;

    [Header("Time LostLife")]
    [SerializeField] private float timer = 0;

    //private ControllerMatriz cMatriz = null;

    void FixedUpdate() {
        if (life > limitLife)
            life = limitLife;    
    }

    public void LostLifeForSecond() {
        if (!Keys.DontMove()) {
            timer += Time.deltaTime;
            if (timer > delayLostLife) {
                life -= valueUseLife;
                timer = 0;
            }
         }
    }

    public void DamageOnPlayer(float damage) {
        StartCoroutine(Camera.main.GetComponent<CameraShake>().Shake(.17f,.4f));

        life -= damage;
    }

    #region Methods Get / Set
    public float Life {
        get{ return life; }
        set { life = value; }
    }

    public float LimitLife {
        get { return limitLife; }
        set { limitLife = value; }
    }

    public float LostLifeDelay{
        get { return delayLostLife; }
        set { delayLostLife = value; }
    }
    #endregion
}

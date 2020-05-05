using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ControllerAttacks : MonoBehaviour {
    [Header("Attack Prefab")]
    public pushNives push;
    public jumpAttackPlayer JumpAttack;
    public dashPlayer Dash;
    public throwPlayer Throw;
    [Header("Damage Player")]
    [SerializeField] private float damageAttacks = 5f;
    [SerializeField] private float impactAttacks = 5f;

    public float Damage {
        get { return damageAttacks; }
        set { damageAttacks = value; }
    }
    public float ImpactForce {
        get { return impactAttacks; }
        set { impactAttacks = value; }
    }

    [Serializable]
    public struct jumpAttackPlayer {
        public jumpAttackNiveis niveis;
        public GameObject target_prefab;
        public float maxRadiusToAttack;
        public GameObject impact_prefab;
        private bool isDamage;
        public bool Damage {
            get { return isDamage; }
            set { isDamage = value; }
        }

    }
    [Serializable]
    public struct dashPlayer {
        public dashNiveis niveis;
        public Vector3 impulse;
        public float[] timeReload;

        public float TimeReload() {
            switch (niveis) {
                case dashNiveis.Basico:
                    return timeReload[0];
                case dashNiveis.Aprimorado:
                    return timeReload[1];
                case dashNiveis.Mestre:
                    return timeReload[2];
            }

            return 0;
        }
    }

    [Serializable]
    public struct throwPlayer {
        public throwNiveis niveis;
        public Transform target;
        public GameObject objectAttack;
        public float radius;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerEnemies : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1.3f;
    [SerializeField] private float gravity = 10f;
    [Header("Attack")]
    [SerializeField] private GameObject attack_prefab = null;
    private bool isSee = false;
    private bool isAttack = true;
    [SerializeField] private Transform pivot_attack = null;
    [Header("Direction")]
    private Vector3 spawn = Vector3.zero;
    [SerializeField] private Vector3 moveDirection = Vector3.zero;
    [Space(2)]
    [SerializeField] private float maxRadius = 0;
    [Header("Life")]
    [SerializeField] private float life = 5f;
    [Header("Death Animation")]
    [SerializeField] private Color colorMain = Color.white;
    [SerializeField] private Color colorEmission = Color.white;
    [SerializeField] private GameObject gosmaDeath = null;
    private GameObject gostaDeath_insta = null;

    private Transform player = null;

    private CharacterController charController = null;
    private Animator anim = null;
    private Material enemiesRenderer = null;

    private ControllerEnemiesDrop eDrop;

    [SerializeField] private TypeEnemies typeEnemies = TypeEnemies.Simple;

    Vector3 enemiesPos = Vector3.zero;
    Vector3 playerPos = Vector3.zero;

    private Vector3 impact = Vector3.zero;
    private float mass = 3.0f;
    void OnEnable() {
        charController = GetComponent<CharacterController>();
        eDrop = GetComponent<ControllerEnemiesDrop>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        enemiesRenderer = transform.GetChild(1).GetComponent<Renderer>().material;

        moveDirection.x = .1f;
        spawn = transform.position;

        isSee = false;
    }

    void Update() {
        CheckImpactPlayer();

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Death") && transform.GetChild(1).gameObject.activeInHierarchy) {
            StartCoroutine(DelayDeath(anim.GetCurrentAnimatorStateInfo(0).length - 0.07f));
        }
    }
    void FixedUpdate() {
        CheckPlayerNear();
    }


    void Move() {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
            anim.speed = moveSpeed;

        anim.SetInteger("do_walk", 1);

        
        

        if (!isSee) {
            float dis = Vector3.Distance(transform.position, spawn);
            if (dis > 0.5)
                moveDirection.x *= -1;

            transform.localRotation = Quaternion.Euler(0, (moveDirection.x * 10) * 90, 0);
        } else {
            moveDirection.x = transform.TransformDirection(Vector3.forward).x;
            moveDirection.z = transform.TransformDirection(Vector3.forward).z;

            Quaternion slerp = Quaternion.LookRotation(player.position - transform.position);

            transform.localRotation = Quaternion.Slerp(transform.rotation, slerp, 5 * Time.fixedDeltaTime);
        }

        
    }

    private void CheckImpactPlayer() {
        if (life > 0) {
            if (impact.magnitude > 0.2f)
                charController.Move(impact * Time.deltaTime);

            impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
        }
    }

    private void AddImpact(Vector3 dir, float force) {
        dir.Normalize();
        if (dir.y < 0) dir.y = -dir.y;
        impact += dir.normalized * force / mass;
    }

    void CheckPlayerNear() {
        if (life > 0) {
            if (!charController.isGrounded)
                moveDirection.y -= gravity * Time.deltaTime;

            charController.Move(moveDirection * moveSpeed * Time.fixedDeltaTime);

            if (InFov(maxRadius)) {
                if (player.GetComponent<ControllerHealth>().Life > 0) {
                    switch (typeEnemies) {
                        case TypeEnemies.Simple:
                            AttackSimples();
                            break;
                        case TypeEnemies.Strong:
                            AttackStrong();
                            break;
                    }
                } else {
                    Move();
                }
            } else {
                Move();
            }
        } else {
            anim.SetInteger("do_walk", 0);

            charController.enabled = false;

            /*Color color = Color.Lerp(enemiesRenderer.GetColor("Color_5590CD4E"), colorMain, Time.deltaTime * 2);
            Color colorE = Color.Lerp(enemiesRenderer.GetColor("Color_1D3630E9"), colorEmission, Time.deltaTime * 2);
            enemiesRenderer.SetColor("Color_5590CD4E", color);
            enemiesRenderer.SetColor("Color_1D3630E9", colorE);*/

            anim.SetInteger("do_death", 1);
        }
    }

    IEnumerator DelayDeath(float value) {
        yield return new WaitForSeconds(value);

        if (!gostaDeath_insta) {
            gostaDeath_insta = Instantiate(gosmaDeath, transform);
            GameObject d = Instantiate(eDrop.PowerUpPrefab, new Vector3(transform.position.x, transform.position.y + 0.1f ,transform.position.z),eDrop.PowerUpPrefab.transform.rotation);
            d.transform.SetParent(GameObject.FindGameObjectWithTag("Scenarios").transform);
        }

        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);

        Destroy(gostaDeath_insta, 2.1f);
        Destroy(gameObject, 2.1f);
    }

    /* Overlap Collider
    bool inFov(float maxAngle , float maxRadius) {
        Collider[] overlaps = new Collider[10];
        enemiesPos = transform.position;
        playerPos = player.position;

        enemiesPos.y += 0.1f;
        playerPos.y += 0.1f;

        int count = Physics.OverlapSphereNonAlloc(enemiesPos, maxRadius, overlaps);

        for (int i = 0;i < count; i++) {
            if (overlaps[i] != null) {
                if (overlaps[i].transform == player) {
                    Vector3 directionBetween = (playerPos - enemiesPos).normalized;
                    directionBetween.y *= 0;

                    float angle = Vector3.Angle(transform.forward, directionBetween);
                    if (angle <= maxAngle) {
                        Ray ray = new Ray(enemiesPos, playerPos - enemiesPos);
                        RaycastHit hit;

                        int layerMask = 1 << 8;

                        if (Physics.Raycast(ray, out hit, maxRadius, layerMask)) {
                            if (hit.transform == player) {
                                return true;
                            }
                        }
                    }
                }
            }
        }
        return false;
    }*/

    private bool InFov(float maxRadius) {
        float dist = Vector3.Distance(transform.position, player.position);
        if(dist < maxRadius) {
            return true;
        }
        return false;
    }

    void AttackSimples() {
        anim.SetInteger("do_walk", 0);
        moveDirection.z = 0;
        moveDirection.x = 0;

        Vector3 lookVector = player.transform.position - transform.position;
        lookVector.y = transform.position.y;
        Quaternion rot = Quaternion.LookRotation(lookVector);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, 30 * Time.fixedDeltaTime);

        if (isAttack) {

            anim.SetInteger("do_attack", 1);

            if (anim.GetInteger("do_attack") == 1) {
                GameObject insta = Instantiate(attack_prefab, pivot_attack.transform.position, Quaternion.identity);
                insta.GetComponent<EnemiesAttack>().PlayerPosition = transform.TransformDirection(Vector3.forward);
            }
            
            StartCoroutine(DelayAttack(1.5f));

            isAttack = false;
        }
    }

    void AttackStrong() {
        isSee = true;
        maxRadius = .4f;
        anim.SetInteger("do_walk", 0);

        if (isAttack) {
            anim.SetInteger("do_attack", 1);

            transform.GetChild(0).GetChild(3).GetChild(1).GetComponent<SphereCollider>().enabled = true;

            StartCoroutine(DelayAttack(1));

            isAttack = false;
        }
    }

    IEnumerator DelayAttack(float value) {
        yield return new WaitForSeconds(value);
        if (typeEnemies == TypeEnemies.Strong)
            transform.GetChild(0).GetChild(3).GetChild(1).GetComponent<SphereCollider>().enabled = false;
        isAttack = true;
    }

    public Animator Animator {
        get { return anim; }
    }

    public Vector3 Direction {
        get { return moveDirection; }
    }

    public void Hit(float damage,Vector3 dir, float impactForce) {
        life -= damage;

        AddImpact(dir, impactForce);
    }

    public TypeEnemies TypeEnemies {
        get { return typeEnemies; }
    }
}

public enum TypeEnemies { Simple, Strong}

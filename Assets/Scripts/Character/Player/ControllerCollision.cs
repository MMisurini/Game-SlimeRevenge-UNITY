using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCollision : MonoBehaviour
{
    [SerializeField] private float impactForce = 3f;
    [SerializeField] private float forceToPush = 10.5f;
    private Vector3 dir;
    private ControllerMatriz cMatriz;

    public bool pressFtoTalk = false;
    public bool pressFtoTriggerLever = false;

    void Start() {
        cMatriz = GetComponent<ControllerMatriz>();
    }

    public void JumpAttack(float radius, float multiplayerForceImpact) {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        int i = 0;
        while (i < hitColliders.Length) {
            if(hitColliders[i].tag == "Enemies")
                hitColliders[i].GetComponent<ControllerEnemies>().Hit(cMatriz.Attack.Damage * multiplayerForceImpact, transform.TransformDirection(Vector3.forward), impactForce * multiplayerForceImpact);

            i++;
        }

        //cMatriz.Moviment = true;
    }

    public GameObject Area(float radius) {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        int i = 0;
        while (i < hitColliders.Length) {
            if (hitColliders[i].tag == "Push") {
                if (Input.GetKeyDown(KeyCode.F))
                    hitColliders[i].GetComponent<Rigidbody>().isKinematic = !hitColliders[i].GetComponent<Rigidbody>().isKinematic;

                return hitColliders[i].gameObject;
            }else if (hitColliders[i].tag == "Throw") {
                if (Input.GetKeyDown(KeyCode.F)) {
                    cMatriz.Attack.Throw.objectAttack = Resources.Load("CubeThrow") as GameObject;

                    //cMatriz.Attack.Throw.objectAttack = hitColliders[i].gameObject;
                    Destroy(hitColliders[i].gameObject);
                }

                return null;
            }else if (hitColliders[i].tag == "Anciao") {
                if (!pressFtoTalk) {
                    cMatriz.Text.EnableTutorial(3, 3);
                }

                if (Input.GetKeyDown(KeyCode.F)) {
                    pressFtoTalk = true;
                    hitColliders[i].GetComponent<OldmanTeacher>().VideoTutorial();

                    cMatriz.Moviment = false;
                }

                break;
            }else if (hitColliders[i].name == "Alavanca") {
                if (!pressFtoTriggerLever) {
                    cMatriz.Text.EnableTutorial(0, 3);
                }

                if (Input.GetKeyDown(KeyCode.F)) {
                    pressFtoTriggerLever = true;
                    hitColliders[i].GetComponent<AlavancaDropBox>().IsOn();
                }

                return null;
            }

            i++;
        }

        return null;
    }

    void OnControllerColliderHit(ControllerColliderHit hit) {
        CollisionPush(hit);
    }

    void CollisionPush(ControllerColliderHit hit) {
        Rigidbody body = hit.collider.attachedRigidbody;

        if (body != null) {
            if(hit.normal == new Vector3(0,1.0f,0)) { cMatriz.Push.Above = true; } else { cMatriz.Push.Above = false; }
            if (!body.isKinematic && cMatriz.Push.Enable) {

                dir = transform.position - hit.gameObject.transform.position;
                dir = -dir.normalized;

                body.AddForce(dir * forceToPush, ForceMode.Force);
            }
        }
        //body.AddForceAtPosition(hit.controller.velocity * 2, hit.point);//body.velocity += hit.controller.velocity;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.name == "Water") {
            cMatriz.cMoviment.Direction = new Vector3(0,-0.1f,0);

            var main = cMatriz.Death.EffectPrefab.GetComponent<ParticleSystem>().main;
            main.gravityModifier = 0.005f;
            main.startSpeed = 0.15f;

            var main2 = cMatriz.Death.EffectPrefab.transform.GetChild(0).GetComponent<ParticleSystem>().main;
            main2.gravityModifier = 0.005f;
            main2.startSpeed = 0.15f;

            cMatriz.Health.Life = -5;
        }
    }
}

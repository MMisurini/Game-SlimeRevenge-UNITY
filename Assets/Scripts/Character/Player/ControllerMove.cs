using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMove : MonoBehaviour
{
    private ControllerMatriz cMatriz = null;

    [SerializeField] private float moveSpeed = 1.5f;
    [SerializeField] private float rotateSpeed = 5f;
    [SerializeField] private float gravity = 0f;
    [SerializeField] private float jumpSpeed = 8;
    [Header("Direction")]
    [SerializeField] private Vector3 moveDirection = Vector3.zero;
    [Header("Move Mouse Or Keybord")]
    [SerializeField] private bool isMouse = false;
    private bool disableControlMove = true;

    private CharacterController charController = null;

    private Vector3 floorPosition = Vector3.zero;

    private int directionRotate = 0;
    // Impact
    private float mass = 3.0f;
    private Vector3 impact = Vector3.zero;
    private Vector3 force = Vector3.zero;
    private GameObject effectImpactGround = null;

    void OnEnable() {
        charController = GetComponent<CharacterController>();
        cMatriz = GetComponent<ControllerMatriz>();
    }

    public void MoveAndJump(bool isMove) {
        Crouch();
        if (isMove) {

            if (Input.GetKeyDown(KeyCode.Space))
                if(!cMatriz.Animation.CurrentStateInfo(0).IsName("Attack1"))
                    if(!cMatriz.Push.Enable)
                        Jump();

            if (isMouse) {
                CheckInputAxis(disableControlMove);
            } else {
                CheckInputMouse(disableControlMove);
            }
            

            CheckImpactPlayer();
            CheckForcePlayer();
        } else {
            ResetInputs();
        }

        cMatriz.Health.LostLifeForSecond();
    }

    private void CheckImpactPlayer() {
        if (impact.magnitude > 0.2f)
            charController.Move(impact * Time.deltaTime);

        impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
    }

    private void CheckForcePlayer() {
        cMatriz.Attack.JumpAttack.Damage = false;

        if (force.magnitude > 0.2f) {
            charController.Move(force * Time.deltaTime);

            cMatriz.Attack.JumpAttack.Damage = true;
        }

        if (charController.isGrounded) {
            force = Vector3.zero;

            if (cMatriz.Attack.JumpAttack.Damage) {
                cMatriz.Moviment = false;

                cMatriz.Health.DamageOnPlayer(25);

                if (effectImpactGround == null)
                    effectImpactGround = Instantiate(cMatriz.Attack.JumpAttack.impact_prefab, transform.position, cMatriz.Attack.JumpAttack.impact_prefab.transform.rotation);

                switch (cMatriz.Attack.JumpAttack.niveis) {
                    case jumpAttackNiveis.Basico:
                        cMatriz.Collision.JumpAttack(.3f, 1);
                        break;
                    case jumpAttackNiveis.Aprimorado:
                        cMatriz.Collision.JumpAttack(.7f, 2);
                        break;
                    case jumpAttackNiveis.Mestre:
                        cMatriz.Collision.JumpAttack(1.3f, 3);
                        break;

                }

                cMatriz.Animation.SetjumpAttack(0);
                effectImpactGround = null;
            }
        }
    }

    private void CheckInputAxis(bool disable) {
        if (Input.GetKey(KeyCode.D)) 
            if(disable)
                transform.Rotate(Vector3.up * directionRotate * rotateSpeed * Time.fixedDeltaTime);
        
        if (Input.GetKey(KeyCode.A)) 
            if(disable)
                transform.Rotate(Vector3.down * directionRotate * rotateSpeed * Time.fixedDeltaTime);

        if (Input.GetKey(KeyCode.W)) {
            cMatriz.Gosma.isWalk = true;

            moveSpeed = 1.5f;
            cMatriz.Animation.SetWalk(1, moveSpeed);
            moveDirection.x = transform.TransformDirection(Vector3.forward).x;
            moveDirection.z = transform.TransformDirection(Vector3.forward).z;
            directionRotate = 1;
        }

        if (Input.GetKey(KeyCode.S)) {
            cMatriz.Gosma.isWalk = true;

            moveSpeed = 1f;
            cMatriz.Animation.SetWalkBack(1, .75f);
            moveDirection.x = transform.TransformDirection(Vector3.back).x;
            moveDirection.z = transform.TransformDirection(Vector3.back).z;
            directionRotate = -1;
        }

        if (Keys.DontMove()) {
            moveDirection.z = 0;
            moveDirection.x = 0;
            cMatriz.Animation.SetWalk(0, moveSpeed);
            cMatriz.Animation.SetWalkBack(0, moveSpeed);
            cMatriz.Gosma.isWalk = false;
        }

    }
    private void CheckInputMouse(bool disable) {
        if (Input.GetKey(KeyCode.D)) {
            if (!cMatriz.Push.Enable) {
                cMatriz.Gosma.isWalk = true;

                moveSpeed = 1.5f;
                cMatriz.Animation.SetWalk(1, moveSpeed);
                moveDirection.x = transform.TransformDirection(Vector3.right).x;
                moveDirection.z = transform.TransformDirection(Vector3.right).z;
                directionRotate = 1;
            }
        }

        if (Input.GetKey(KeyCode.A)) {
            if (!cMatriz.Push.Enable) {
                cMatriz.Gosma.isWalk = true;

                moveSpeed = 1.5f;
                cMatriz.Animation.SetWalk(1, moveSpeed);
                moveDirection.x = transform.TransformDirection(Vector3.left).x;
                moveDirection.z = transform.TransformDirection(Vector3.left).z;
                directionRotate = 1;
            }
        }

        if (Input.GetKey(KeyCode.W)) {
            cMatriz.Gosma.isWalk = true;

            moveSpeed = 1.5f;

            if (cMatriz.Animation.GetCrounch() == 0)
                if (!cMatriz.Push.Enable)
                    cMatriz.Animation.SetWalk(1, moveSpeed);
                else
                    cMatriz.Animation.SetWalkPush(1, moveSpeed);
            else
                cMatriz.Animation.SetWalkCrouch(1, moveSpeed);


            moveDirection.x = transform.TransformDirection(Vector3.forward).x;
            moveDirection.z = transform.TransformDirection(Vector3.forward).z;
            directionRotate = 1;
        }

        if (Input.GetKey(KeyCode.S)) {
            cMatriz.Gosma.isWalk = true;

            moveSpeed = 1f;
            cMatriz.Animation.SetWalkBack(1, .75f);
            moveDirection.x = transform.TransformDirection(Vector3.back).x;
            moveDirection.z = transform.TransformDirection(Vector3.back).z;
            directionRotate = -1;
        }

        if (Keys.DontMoveKeyWithMouse()) {
            moveDirection.z = 0;
            moveDirection.x = 0;
            cMatriz.Animation.SetWalk(0, moveSpeed);
            cMatriz.Animation.SetWalkBack(0, moveSpeed);
            cMatriz.Animation.SetWalkCrouch(0, moveSpeed);
            cMatriz.Animation.SetWalkPush(0, moveSpeed);
            cMatriz.Gosma.isWalk = false;
        }

        if(!cMatriz.Animation.CurrentStateInfo(0).IsName("Attack1"))
            if(disable)
                RotatePlayerWithMouse();
    }

    private void RotatePlayerWithMouse() {

        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, new Vector3(0,transform.position.y,0));
        //RaycastHit rayLenght;
        float rayFloat = 0f;

        //if (Physics.Raycast(cameraRay, out rayLenght)) {
        if (groundPlane.Raycast(cameraRay, out rayFloat)) {
            //Vector3 pointToLook = rayLenght.point;
            Vector3 pointToLook = cameraRay.GetPoint(rayFloat);

            Debug.DrawLine(cameraRay.origin, pointToLook, Color.green);

            //transform.LookAt(pointToLook);

            Quaternion slerp = Quaternion.LookRotation(pointToLook - transform.position) ;
            slerp.x = 0;
            slerp.z = 0;

            transform.localRotation = Quaternion.Slerp(transform.rotation, slerp, 6 * Time.fixedDeltaTime);
        }
    }

    private void ResetInputs() {
        moveDirection.z = 0;
        moveDirection.x = 0;
        moveSpeed = 1.5f;
        directionRotate = 1;
    }

    public void CheckMovimentPlayer() {
        if (!charController.isGrounded)
            moveDirection.y -= gravity * Time.deltaTime;
        else
            floorPosition = transform.position;

        if (charController.enabled)
            charController.Move(moveDirection * moveSpeed * Time.fixedDeltaTime);
        else
            moveDirection = Vector3.zero;
    }

    public void AddImpact(Vector3 dir, float force) {
        dir.Normalize();
        //if (dir.y < 0) dir.y = -dir.y;
        impact += dir.normalized * force / mass;
    }

    public void AddForceDirection(Vector3 dir, float forces) {
        force += dir.normalized * forces / mass;
    }

    public void Jump() {
        if (charController.isGrounded) {
            cMatriz.Animation.SetJump(true);
            moveDirection.y = jumpSpeed;
        }
    }

    void Crouch() {
        if (Input.GetKey(KeyCode.LeftShift)) {
            if (charController.isGrounded) {
                if(!cMatriz.Push.Enable)
                    cMatriz.Animation.SetCrouch(1);
            }
        } else {
            cMatriz.Animation.SetCrouch(0);
        }
    }

    public Vector3 Direction {
        get { return moveDirection; }
        set { moveDirection = value; }
    }
    public float Gravity {
        get { return gravity; }
        set { gravity = value; }
    }
    public CharacterController CharController {
        get { return charController; }
    }

    public Vector3 FloorPosition {
        get { return floorPosition; }
    }

    public bool DisableMouseAndKeybord {
        get { return disableControlMove; }
        set { disableControlMove = value; }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMatriz : MonoBehaviour
{
    [SerializeField] private Color cMaterialMain = Color.white;
    [SerializeField] private Color cMaterialEmission = Color.white;

    public Vector3 spawn = Vector3.zero;

    private ControllerAnimation cAnim = null;
    private ControllerMove cMove = null;
    private ControllerDeath cDeath = null;
    private ControllerHealth cHealth = null;
    private ControllerGosma cGosma = null;
    private ControllerAttacks cAttack = null;
    private ControllerCollision cCollision = null;
    private ControllerPush cPush = null;
    private ControllerText cText = null;
    private ControllerItens cItens = null;

    private Menu cMenu = null;

    [Header("Started Scale Player")]
    [SerializeField] private Vector3 startedScale = Vector3.zero;
    [Header("Moviment")]
    [SerializeField]private bool isMoviment = true;

    private bool isStayMenu = false;

    /* jumpAttack *******************************************/
    private GameObject jumpAttackInsta = null;
    private Vector3 dir = Vector3.zero;

    /* Dash *************************************************/
    public bool isCanDash = true;

    void Start() {
        cAnim = GetComponent<ControllerAnimation>();    
        cMove = GetComponent<ControllerMove>();
        cDeath = GetComponent<ControllerDeath>();
        cHealth = GetComponent<ControllerHealth>();
        cGosma = GetComponent<ControllerGosma>();
        cAttack = GetComponent<ControllerAttacks>();
        cCollision = GetComponent<ControllerCollision>();
        cPush = GetComponent<ControllerPush>();
        cText = GetComponent<ControllerText>();
        cItens = GetComponent<ControllerItens>();

        cMenu = Master.main.Menu;

        transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Renderer>().material.SetColor("Color_5590CD4E", cMaterialMain);
        transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Renderer>().material.SetColor("Color_F6DE7F4C", cMaterialEmission);

        startedScale = transform.GetChild(0).GetChild(0).localScale;

        spawn = new Vector3(-.25f,.004f,.14f);
    }

    void FixedUpdate() {
        cMove.CheckMovimentPlayer();
    }

    void Update(){
        if (cMenu.Game) {
            cMove.MoveAndJump(isMoviment);

            cCollision.Area(.8f);

            DeathAnimation();
            HealthAnimation();

            #region Skills
            SimpleAttackAnimation();
            JumpAttackAnimation(cAttack.JumpAttack.niveis);
            DashAnimation(cAttack.Dash.niveis);
            ThrowAnimation(cAttack.Throw.niveis);
            #endregion
        } else {
            StayAnimation();    
        }
    }



    #region Methods
    private void SimpleAttackAnimation() {
        if (Input.GetMouseButtonDown(0)) {
            if (cMove.CharController.isGrounded)
                if (!cAnim.CurrentStateInfo(0).IsName("Attack1") && !cAnim.CurrentStateInfo(0).IsName("CrouchIn") && !cAnim.CurrentStateInfo(0).IsName("Walk Crouch"))
                    cAnim.SetAttack(1);
        }
    }

    private void StayAnimation() {
        if (!isStayMenu) {
            StartCoroutine(AnimationMenu());

            isStayMenu = true;
        }
        cAnim.ResetIdle();
    }

    IEnumerator AnimationMenu() {
        yield return new WaitForSeconds(Random.Range(10, 25));

        if (!cMenu.Game) {
            cAnim.SetIdle();
            isStayMenu = false;
        }
    }
    private void DeathAnimation() {
        if (cHealth.Life <= 0) {
            cDeath.Death();            
            isMoviment = false;
        }
    }
    private void HealthAnimation() {
        Vector3 scalePlayer = transform.GetChild(0).GetChild(0).localScale;
        Transform transformPlayer = transform.GetChild(0).GetChild(0);

        if (IsBetween(cHealth.Life, 101, cHealth.LimitLife)) { // 101 - var
            float targetValue = startedScale.x * (20f / 100f);
            Vector3 value = new Vector3(startedScale.x + targetValue, startedScale.y + targetValue, startedScale.z + targetValue);
            HealthAnimationScale(transformPlayer, value, .7f);
        } else if (IsBetween(cHealth.Life, 76, 100)) { // 76 - 100
            HealthAnimationScale(transformPlayer, startedScale, .7f);
        } else if (IsBetween(cHealth.Life, 51, 75)) { // 51 - 75
            float targetValue = startedScale.x * (20f / 100f);
            Vector3 value = new Vector3(startedScale.x - targetValue, startedScale.y - targetValue, startedScale.z - targetValue);
            HealthAnimationScale(transformPlayer, value, .7f);
        } else if(IsBetween(cHealth.Life, 26,50)) { // 26 - 50
            float targetValue = startedScale.x * (30f / 100f);
            Vector3 value = new Vector3(startedScale.x - targetValue, startedScale.y - targetValue, startedScale.z - targetValue);
            HealthAnimationScale(transformPlayer, value, .7f);
        }else if (IsBetween(cHealth.Life, 11, 25)) { // 11 - 25
            float targetValue = startedScale.x * (35f / 100f);
            Vector3 value = new Vector3(startedScale.x - targetValue, startedScale.y - targetValue, startedScale.z - targetValue);
            HealthAnimationScale(transformPlayer, value, .7f);
        } else if (IsBetween(cHealth.Life, 0, 10)) {// 0 - 10
            float targetValue = startedScale.x * (50f / 100f);
            Vector3 value = new Vector3(startedScale.x - targetValue, startedScale.y - targetValue, startedScale.z - targetValue);
            HealthAnimationScale(transformPlayer, value, .7f);
        }

    }
    private void JumpAttackAnimation(jumpAttackNiveis niveis) {
        if (niveis != jumpAttackNiveis.Null) {
            if (Input.GetMouseButton(0)) {
                if (!cMove.CharController.isGrounded) {
                    cMove.DisableMouseAndKeybord = false;
                    Time.timeScale = 0.5f;

                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit)) {
                        if (jumpAttackInsta == null) {
                            jumpAttackInsta = Instantiate(cAttack.JumpAttack.target_prefab, hit.point, cAttack.JumpAttack.target_prefab.transform.rotation);
                        } else {
                            jumpAttackInsta.transform.position = new Vector3(hit.point.x, 0.012f, hit.point.z);
                        }

                        float distance = Vector3.Distance(transform.position, jumpAttackInsta.transform.position);

                        dir = (hit.point - transform.position).normalized;


                        if (distance > cAttack.JumpAttack.maxRadiusToAttack) {
                            Destroy(jumpAttackInsta);
                            dir = Vector3.zero;
                        }
                    }
                } else {
                    cMove.DisableMouseAndKeybord = true;

                    if (Time.timeScale != 1)
                        Time.timeScale = 1;

                    if (jumpAttackInsta != null)
                        Destroy(jumpAttackInsta);
                }
            } else if (Input.GetMouseButtonUp(0)) {
                if (!cMove.CharController.isGrounded) {
                    Time.timeScale = 1;

                    if (jumpAttackInsta != null) {
                        cAnim.SetjumpAttack(1);

                        if (dir != Vector3.zero)
                            cMove.AddForceDirection(dir, 40f);
                    } else {
                        cMove.DisableMouseAndKeybord = true;
                    }

                    Destroy(jumpAttackInsta);
                }
            }
        }
    }
    private void DashAnimation(dashNiveis niveis) {
        if (niveis != dashNiveis.Null) {
            if (Input.GetKeyDown(KeyCode.Q)) {
                if (!cPush.Enable) {
                    if (isCanDash) {
                        Vector3 dir = transform.TransformDirection(Vector3.forward);

                        cMove.CharController.detectCollisions = false;

                        dir.Normalize();
                        cAttack.Dash.impulse += dir.normalized * 10;

                        StartCoroutine(ReloadDash(cAttack.Dash.TimeReload()));

                        isCanDash = false;
                    }
                }
            }


            if (cAttack.Dash.impulse.magnitude > 0.1f) 
                cMove.CharController.Move(cAttack.Dash.impulse * Time.deltaTime);

            cAttack.Dash.impulse = Vector3.Lerp(cAttack.Dash.impulse, Vector3.zero, 5 * Time.deltaTime);

        }
    }
    private void ThrowAnimation(throwNiveis niveis) {
        if (niveis != throwNiveis.Null) {
            if (Input.GetMouseButton(1)) {
                if (!cPush.Enable && cMove.CharController.isGrounded) {
                    if(cAttack.Throw.objectAttack != null)
                        cAnim.SetThrow(1);
                }
            }

            cCollision.Area(cAttack.Throw.radius);
        }
    }
    IEnumerator ReloadDash(float value) {
        yield return new WaitForSeconds(value);
        isCanDash = true;
    }

    public bool IsBetween(float testValue, float bound1, float bound2) {
        if (bound1 > bound2) {
            return testValue >= bound2 && testValue <= bound1;
        }
        return testValue >= bound1 && testValue <= bound2;
    }

    void HealthAnimationScale(Transform target,Vector3 value, float speed) {
        target.localScale = Vector3.Lerp(target.localScale, new Vector3(value.x, value.y, value.z), speed * Time.deltaTime);
    }

    #endregion

    #region Method Get Set
    public bool Moviment {
        set { isMoviment = value; }
        get { return isMoviment; }
    }

    public ControllerMove cMoviment {
        get { return cMove; }
    }

    public ControllerAnimation Animation {
        get { return cAnim; }
    }

    public ControllerGosma Gosma {
        get { return cGosma; }
    }

    public ControllerAttacks Attack {
        get { return cAttack; }
    }

    public ControllerHealth Health {
        get { return cHealth; }
    }

    public ControllerCollision Collision {
        get { return cCollision; }
    }

    public ControllerPush Push {
        get { return cPush; }
    }

    public ControllerDeath Death {
        get { return cDeath; }
    }
    public ControllerText Text {
        get { return cText; }
    }
    public ControllerItens Itens {
        get { return cItens; }
    }

    public Menu Menu {
        get { return cMenu; }
    }
    public Color ColorMainStarted {
        get { return cMaterialMain; }
    }
    public Color ColorEmissionStarted {
        get { return cMaterialEmission; }
    }
    #endregion
}

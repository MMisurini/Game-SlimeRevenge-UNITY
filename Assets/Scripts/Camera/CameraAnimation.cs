using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimation : MonoBehaviour {
    [SerializeField] private Transform target;
    public Vector3 offSetPos;
    private Vector3 velocity = Vector3.zero;
    public float speedAnimation = 1f;
    public float speedRotation = 1f;
    public float maxRadius = 4f;

    private CameraClamp camClamp = null;

    [Space(10)]
    [SerializeField] private List<Transform> waypoints = null;
    [SerializeField] private int index = 0;

    private Animation anim = null;

    [SerializeField] private bool isAnim;

    private Vector3 nextDelta = Vector3.zero;
    private Transform startPosition = null;

    private bool isAnimationMenu = false;
    void Start() {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        for (int i = 0;i < GameObject.FindGameObjectsWithTag("Waypoints").Length;i++) {
            waypoints.Add(GameObject.FindGameObjectsWithTag("Waypoints")[i].transform);
        }

        if (!target.GetComponent<ControllerMatriz>().Menu.Game) {
            camClamp = GetComponent<CameraClamp>();
            camClamp.enabled = false;

            isAnim = false;
        } else {
            camClamp = GetComponent<CameraClamp>();
            camClamp.enabled = true;

            this.enabled = false;

            isAnim = true;
        }

        if (anim == null)
            anim = Master.main.Canvas.GetComponent<Animation>();

        startPosition = transform;
    }

    int Index(int index) {
        if (index >= waypoints.Count)
            return index - 1;

        return index;
    }

    void Update() {
        Vector3 targetPos = target.position;

        targetPos.x += offSetPos.x;
        targetPos.y += offSetPos.y;
        targetPos.z += offSetPos.z;

        if (!isAnim) {
            if (inFov(maxRadius)) {
                isAnim = true;
            } else {
                Real();
            }
        } else {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speedAnimation);
            transform.LookAt(target);

            if (!isAnimationMenu) {
                anim.Play("Menu");
                isAnimationMenu = !isAnimationMenu;
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
        Gizmos.DrawWireSphere(transform.position, maxRadius);
    }

    void Real() {
        float dst = Vector3.Distance(transform.position, waypoints[index].position);

        transform.position = Vector3.MoveTowards(transform.position, waypoints[index].position, Time.deltaTime * speedAnimation);

        Quaternion rotation = Quaternion.LookRotation(waypoints[Index(index + 1)].position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speedRotation);

        if (dst < 1f) {
            index++;
        }
        if (index >= waypoints.Count) {
            index = 0;
        }
    }
    private float progressDistance = 0f;
    private float currentSpeed = 10f;

    private  Vector3 nextPosition = Vector3.zero;
    void Test() {
        //progressDistance += Time.deltaTime * .8f;
        nextDelta = GetPosition();

        float dst = Vector3.Distance(transform.position, waypoints[index].position);

        if (nextDelta.magnitude < 10) {
            index++;
        }
        if (index >= waypoints.Count) {
            index = 0;
        }

        nextDelta.Normalize(); // Set our direction vector to exactly 1 in magnitude.
        nextDelta *= currentSpeed; // Scale it back up to exactly our specified speed.
        transform.position += nextDelta * Time.deltaTime;

        

        //transform.position = Vector3.MoveTowards(transform.position, waypoints[index].position, Time.deltaTime * speedAnimation);

        Quaternion rotation = Quaternion.LookRotation(waypoints[index].position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speedRotation);
    }

    private Vector3 GetPosition() {
        nextPosition = Vector3.Lerp(waypoints[index].position, waypoints[index + 1].position, Time.deltaTime * 50f);
        Vector3 dir = nextPosition - transform.position;

        return dir;
    }

    int IndexTest(int index) {
        if (index == 0) {
            return index;
        }else if (index ==1) {

        }

        return 0;
    }

    bool inFov(float maxRadius) {
        Collider[] overlaps = new Collider[5];

        int layerMask = 1 << 10;
        layerMask |= (1 << LayerMask.NameToLayer("Default"));

        int count = Physics.OverlapSphereNonAlloc(transform.position, maxRadius, overlaps, layerMask);

        for (int i = 0; i < count; i++) {
            if (overlaps[i] != null) {
                if (overlaps[i].tag == "Player") {
                    return true;
                }
            }
        }

        return false;
    }
}

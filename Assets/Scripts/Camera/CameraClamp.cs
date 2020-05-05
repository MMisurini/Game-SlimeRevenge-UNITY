using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClamp : MonoBehaviour
{
    public Transform target;

    public Vector3 velocity = Vector3.zero;

    public float smoothTime = .15f;

    public bool YMaxEnable = false;
    public float YMaxValue = 0;

    public bool YMinEnable = false;
    public float YMinValue = 0;

    public bool XMaxEnable = false;
    public float XMaxValue = 0;

    public bool XMinEnable = false;
    public float XMinValue = 0;

    [Header("Distance Player")]
    public Vector3 offSetPos = Vector3.zero;

    private bool center = false;

    private void Start() {
        
    }

    void Update(){
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player").transform;

        Vector3 targetPos = target.position;

        Vector3 playerLimit = Camera.main.WorldToViewportPoint(targetPos);

        //vertical
        if (YMinEnable && YMaxEnable)
            targetPos.y = Mathf.Clamp(target.position.y, YMinValue, YMaxValue);
        else if (YMinEnable)
            targetPos.y = Mathf.Clamp(target.position.y, YMinValue, target.position.y);
        else if (YMaxEnable)
            targetPos.y = Mathf.Clamp(target.position.y, target.position.y, YMaxValue);

        //horizontal
        if (XMinEnable && XMaxEnable)
            targetPos.x = Mathf.Clamp(target.position.x, XMinValue, XMaxValue);
        else if (XMinEnable)
            targetPos.x = Mathf.Clamp(target.position.x, XMinValue, target.position.x);
        else if (XMaxEnable)
            targetPos.x = Mathf.Clamp(target.position.x, target.position.x, XMinValue);

        targetPos.x += offSetPos.x;
        targetPos.y += offSetPos.y;
        targetPos.z += offSetPos.z;

        if (!center) {
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
        } else {
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);

            float distance = Vector3.Distance(transform.position, targetPos);
            if (distance < 0.1f)
                center = false;
        }

        transform.LookAt(target);
    }

    bool CheckPosition(Vector3 value) {
        if (value.x > 0.7f | value.x < 0.3f) {
            center = true;
            return true;
        }
        return false;
    }
}

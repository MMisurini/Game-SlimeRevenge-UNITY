using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlavancaDropBox : MonoBehaviour
{
    [SerializeField] private GameObject objectToDrop = null;

    [SerializeField] private bool isOn = false;
    private bool onetime = true;
    private float timer = 0;

    void Update() {
        if (isOn) {
            if (onetime) {
                CameraClamp cam = Camera.main.GetComponent<CameraClamp>();
                cam.enabled = false;

                cam.GetComponent<Camera>().transform.LookAt(objectToDrop.transform);
                transform.GetChild(1).localRotation = Quaternion.Lerp(transform.GetChild(1).localRotation, Quaternion.Euler(-140,0,0), Time.deltaTime * 10);
                objectToDrop.GetComponent<Rigidbody>().useGravity = true;

                StartCoroutine(AfterTime(2, cam));
            }
        }
    }

    IEnumerator AfterTime(float value, CameraClamp cam) {
        yield return new WaitForSeconds(value);
        cam.enabled = true;
        onetime = false;
        isOn = false;
    }

    public void IsOn() {
        isOn = true;
    }
}

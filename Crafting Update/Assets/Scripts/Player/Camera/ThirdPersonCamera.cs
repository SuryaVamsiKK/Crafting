using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;

    [Header("Speeds")]
    public float mouseSensitivity = 10f;
    public float camfollowSpeed = 3.5f;

    [Header("ClampValues")]
    public float minDistanceFromTarget = 8;
    public float maxDistanceFromTarget = 20;
    public float minPitch, maxPitch;

    [Header("For other scripts and testing")]
    public float distanceFromTarget = 10;
    public bool toFollowCam = true;

    float yaw, pitch;
    Vector3 targePos;

    private void Start()
    {
        targePos = target.position;
    }

    void FixedUpdate()   
    {
        cameraInput();
        distanceAdjust();
    }

    void cameraInput()
    {
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        Vector3 targetRotation = new Vector3(pitch, yaw);
        targePos = target.position;
        transform.localEulerAngles = targetRotation;
        
        //transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, targetRotation, Time.deltaTime * 5f);

        //transform.position = (target.position - (transform.forward * distanceFromTarget));

        transform.position = Vector3.Lerp(transform.position, targePos - (transform.forward * distanceFromTarget), Time.deltaTime * 5f);
        transform.position = target.TransformPoint(target.InverseTransformPoint(transform.position).normalized * distanceFromTarget);

        Vector3 playerLocal = target.InverseTransformPoint(transform.position);
        playerLocal = new Vector3(playerLocal.x, 0, playerLocal.z);
        Vector3 updatedForward = (targePos - target.TransformPoint(playerLocal)).normalized;
        Quaternion targetRot = Quaternion.LookRotation(updatedForward, target.up);

        if (toFollowCam)
        {
            targetRot = Quaternion.Inverse(target.parent.rotation) * targetRot;
            target.localRotation = Quaternion.Lerp(target.localRotation, targetRot, Time.deltaTime * camfollowSpeed);
            //target.forward = Vector3.Lerp(target.forward, updatedForward, Time.deltaTime * camfollowSpeed);
        }

        transform.parent.localPosition = target.localPosition;
        transform.parent.up = (target.position - transform.parent.parent.parent.position).normalized;
        //transform.parent.localEulerAngles = new Vector3(target.localEulerAngles.x, 0, target.localEulerAngles.z);
    }

    void distanceAdjust()
    {
        distanceFromTarget -= Input.GetAxis("Mouse ScrollWheel");
        distanceFromTarget = Mathf.Clamp(distanceFromTarget, minDistanceFromTarget, maxDistanceFromTarget);
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            transform.parent.localPosition = target.localPosition;
            transform.parent.up = target.up;
        }
    }
}

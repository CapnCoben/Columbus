using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float xRot;
    [SerializeField] public Camera cam;
    [SerializeField] private float sensitivity;
    [SerializeField] private float minCamVeiw, maxCamVeiw;
    void Update()
    {

    
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, minCamVeiw, maxCamVeiw);
        cam.transform.localRotation = Quaternion.Euler(xRot, 0, 0);
        transform.Rotate(Vector3.zero, mouseX);
    }




}

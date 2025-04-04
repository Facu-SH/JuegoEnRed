using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PLayerScripts;

public class FPSCameraMovement : MonoBehaviour
{
    [SerializeField] private BasePLayerStats data;
    
    private Transform playerTransform;
    private float xRotation = 0f;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerTransform = transform.parent;
    }
    
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * data.MouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * data.MouseSensitivity * Time.deltaTime;
        
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerTransform.Rotate(Vector3.up * mouseX);
    }
}

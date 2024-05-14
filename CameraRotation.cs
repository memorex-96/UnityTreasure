using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float rotSpeed = 3f;
    private bool isRot = false;
    private Vector3 initialMousePos; 
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Mouse Button is Pressed");
            isRot = true;
            initialMousePos = Input.mousePosition;
        }else if (Input.GetMouseButtonUp(1))
        {
            Debug.Log("Mouse Button is Released");
            isRot = false; 
        }
        if(isRot)
        {
            float mouseX = Input.GetAxis("Mouse X");
            Vector3 currentMousePos = Input.mousePosition; 

            Vector3 delta = currentMousePos - initialMousePos;
            transform.Rotate(new Vector3(0, delta.x, 0) * rotSpeed * Time.deltaTime);

            initialMousePos = currentMousePos;
        }
         
        
    }
}

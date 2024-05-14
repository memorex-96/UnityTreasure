using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    Quaternion initialRotation;

    void Start()
    {
        initialRotation = transform.rotation;
    }

    void Update()
    {
        //Sprint
        if (Input.GetKeyDown(KeyCode.LeftShift)) 
        {
            speed = 7f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 5f;
        }
        //Rotation

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(new Vector3(0, 90, 0) * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(new Vector3(0, -90, 0) * Time.deltaTime);
        }
       


        Vector3 forwardDirection = initialRotation * transform.forward;
        Vector3 rightDirection = initialRotation * transform.right;

        //Move Left
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= rightDirection * speed * Time.deltaTime;
        }
        //Move Right
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += rightDirection * speed * Time.deltaTime;
        }
        //Move Forwards
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += forwardDirection * speed * Time.deltaTime;
        }
        //Move Backwards
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= forwardDirection * speed * Time.deltaTime;
        }
    }
}

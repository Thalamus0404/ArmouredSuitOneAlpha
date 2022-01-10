using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example_MouseLook : MonoBehaviour
{
    public float sensitivity = 100f;
    public float rotationX;
    public float rotationY;
    public float rotationZ;
    public float rotSpeed = 30f;
    public float rotSpeedX;

    void Update()
    {
        MouseLook(sensitivity);
        if (Input.GetKey(KeyCode.A))
        {
            rotSpeedX = rotSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rotSpeedX = -rotSpeed;
        }
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            rotSpeedX = 0;
        }
    }

    public void MouseLook(float sensitivity)
    {
        float mouseRotationX = Input.GetAxis("Mouse X");
        float mouseRotationY = Input.GetAxis("Mouse Y");        

        rotationX += mouseRotationY * sensitivity * Time.deltaTime;
        rotationY += mouseRotationX * sensitivity * Time.deltaTime;
        rotationZ += rotSpeed * Time.deltaTime;

        transform.Rotate(Vector3.left * mouseRotationY);
        transform.Rotate(Vector3.up * mouseRotationX);
    }
}

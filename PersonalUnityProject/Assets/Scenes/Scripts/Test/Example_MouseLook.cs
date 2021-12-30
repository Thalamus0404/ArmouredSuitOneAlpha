using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example_MouseLook : MonoBehaviour
{
    public float sensitivity = 100f;
    public float rotationX;
    public float rotationY;       

    void Update()
    {
        float mouseRotationX = Input.GetAxis("Mouse X");
        float mouseRotationY = Input.GetAxis("Mouse Y");

        rotationX += mouseRotationY * sensitivity * Time.deltaTime;
        rotationY += mouseRotationX * sensitivity * Time.deltaTime;

        transform.eulerAngles = new Vector3(-rotationX, rotationY, 0);
    }
}

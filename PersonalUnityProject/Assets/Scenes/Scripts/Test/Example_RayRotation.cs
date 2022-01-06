using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example_RayRotation : MonoBehaviour
{
    Vector3 mousePosition;

    public float sensitivity = 100;
    void Update()
    {
        mousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        //Debug.Log(mousePosition);

        float mouseRotationY = (mousePosition.x - 0.5f) * sensitivity * Time.deltaTime;
        float mouseRotationX = (mousePosition.y - 0.5f ) * sensitivity * Time.deltaTime;

        transform.Rotate(Vector3.left * mouseRotationX);
        transform.Rotate(Vector3.up * mouseRotationY);
    }
}

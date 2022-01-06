using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example_Flight : MonoBehaviour
{
    public Transform player;
    public Rigidbody playerRigidbody; 
    public Camera playerCamera;
    public Transform front;

    public float speed = 1000f;
    public float speedX;
    public float acceleration = 0.3f;
    public float rotSpeed = 30f;
    public float rotSpeedX;   
    public float sensitivity = 100f;
    public float rotationX;
    public float rotationY;
    public float rotationZ;

    Vector3 mousePosition;

    void Start()
    {
        player = gameObject.transform;
        playerRigidbody = GetComponent<Rigidbody>();
        front = GameObject.Find("Forward").GetComponent<Transform>();
        speedX = 0.5f * speed;        
    }

    void Update()
    {
        PlayerMove(speed);
        MakeNormal(speed);
        PlayerRotation(sensitivity);        
    }

    public void PlayerMove(float speed)
    {        
        playerRigidbody.velocity = front.transform.forward * speedX * Time.deltaTime;        
        
        if (Input.GetKey(KeyCode.W))
        {
            if (speedX <= speed && !Input.GetKey(KeyCode.LeftShift))
            {
                speedX += acceleration * speed * Time.deltaTime;
            }
            else if(speedX <= 2f * speed && Input.GetKey(KeyCode.LeftShift))
            {
                speedX += 2f * acceleration * speed * Time.deltaTime;
            }
        }        
        if (Input.GetKey(KeyCode.S))
        {
            if (speedX >= 0)
            {
                speedX += -acceleration * speed * Time.deltaTime;
            }            
        }
        if (Input.GetKey(KeyCode.A))
        {
            rotSpeedX = rotSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rotSpeedX = -rotSpeed;
        }
    }

    public void MakeNormal(float speed)
    {
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            if (speedX <= 0.5f * speed)
            {
                speedX += acceleration * speed * Time.deltaTime;
            }
            if (speedX >= 0.5f * speed)
            {
                speedX += -acceleration * speed * Time.deltaTime;
            }
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            rotSpeedX = 0;
        }
    }

    public void PlayerRotation(float sensitivity)
    {

        mousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        float mouseRotationY = (mousePosition.x - 0.5f) * sensitivity * Time.deltaTime;
        float mouseRotationX = (mousePosition.y - 0.5f) * sensitivity * Time.deltaTime;

        transform.Rotate(Vector3.left * mouseRotationX);
        transform.Rotate(Vector3.up * mouseRotationY);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, rotationZ);
    }
}

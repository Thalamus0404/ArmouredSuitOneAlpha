using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example_Flight : MonoBehaviour
{
    public Transform player;
    public Rigidbody playerRigidbody;
    public float speed = 1000f;
    public float speedX;
    public float rotSpeed = 30f;
    public Camera playerCamera;
    public Transform front;

    RaycastHit hit;

    void Start()
    {
        player = gameObject.transform;
        playerRigidbody = GetComponent<Rigidbody>();
        playerCamera = GetComponentInChildren<Camera>();
        front = GameObject.Find("Forward").GetComponent<Transform>();
        speedX = 0.5f * speed;
    }

    void Update()
    {
        //if(Input.GetKey(KeyCode.W))
        //{            
        //    float v = Input.GetAxis("Vertical");
        //    player.transform.Translate(Vector3.forward * v * Time.deltaTime * speed);
        //    Debug.Log(v);
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    float v = Input.GetAxis("Vertical");
        //    player.transform.Translate(Vector3.forward * v * Time.deltaTime * speed);
        //}
        playerRigidbody.velocity = front.transform.forward * speedX * Time.deltaTime;

        if(Input.GetKey(KeyCode.W))
        {
            if(speedX <= speed)
            {
                speedX += 0.1f * speed * Time.deltaTime;
            }        
            else
            {
                speedX = speed;
            }
        }
        if(Input.GetKey(KeyCode.S))
        {
            if(speedX >= 0)
            {
                speedX += -0.1f * speed * Time.deltaTime;
            }
            else
            {
                speedX = 0;
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            player.transform.Rotate(0, 0, rotSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            player.transform.Rotate(0, 0, - rotSpeed * Time.deltaTime);
        }        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example_Flight : MonoBehaviour
{
    public Transform player;
    public Rigidbody playerRigidbody;
    public float speed = 15f;
    public float rotSpeed = 15f;

    RaycastHit hit;

    void Start()
    {
        player = gameObject.transform;
        playerRigidbody = GetComponent<Rigidbody>();
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
        if(Input.GetKey(KeyCode.W))
        {            
            playerRigidbody.velocity = transform.forward * speed * Time.deltaTime;
            Debug.Log(playerRigidbody.velocity);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example_PlayerMove : MonoBehaviour
{
    public float speed = 10f;
    public float speedX;
    public float acceleration = 0.1f;
    public float rotSpeed = 30f;

    void Start()
    {
        speedX = speed;
    }

    void Update()
    {
        PlayerMove(speed);
        Equilibrate(speed);
    }

    public void PlayerMove(float speed)
    {
        transform.Translate(Vector3.forward * speedX * Time.deltaTime);

        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.LeftShift))
        {
            if (speedX <= 1.5f * speed)
            { 
                speedX += acceleration * speed; 
            }            
        }
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
        {
            if (speedX <= 2f * speed)
            {
                speedX += 1.5f * acceleration * speed;
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (speedX >= 0)
            {
                speedX += - acceleration * speed;
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, 0, rotSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, 0, - rotSpeed * Time.deltaTime);
        }
    }

    public void Equilibrate(float speed) //정상 속도로 돌려주는 기능
    {
        if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            if(speedX > speed)
            {
                speedX += -acceleration * speed;
            }
            else if(speedX <= speed)
            {
                speedX += acceleration * speed;
            }
        }
    }
}

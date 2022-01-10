using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example_Collision : MonoBehaviour
{
    Example_PlayerMove playerMove;

    void Start()
    {
        if (gameObject.tag == "Player")
        {
            playerMove = GetComponent<Example_PlayerMove>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "PlayerBullet" || collision.gameObject.tag != "EnemyBullet")
        {
            CollisionBounce(collision);            
        }
    }

    public void CollisionBounce(Collision collision)
    {
        Vector3 dir = collision.transform.position - transform.position;
        int i = 0;
        do
        {
            transform.Translate(Vector3.Reflect(dir, collision.contacts[0].normal) * playerMove.speedX * Time.deltaTime);
            Debug.Log("Ãæµ¹ÇÔ");
        } while (i >= 5);
    }
}

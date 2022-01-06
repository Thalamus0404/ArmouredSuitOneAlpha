using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example_Bullet : MonoBehaviour
{
    public float bulletSpeed = 50f;

    void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Enemy":
                Example_Enemy enemy = other.GetComponent<Example_Enemy>();
                enemy.hp--;                
                break;
            default:
                break;
        }
        Destroy(gameObject);
    }
}

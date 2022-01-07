using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example_Bullet : MonoBehaviour
{    
    public float bulletSpeed = 50f;
    public int damage = 1;

    void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }
}

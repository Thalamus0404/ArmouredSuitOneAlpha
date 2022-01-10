using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example_WeaponShot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 3000f;
    public float coolTime = 1f;
    public float curTime;

    void Start()
    {
        curTime = coolTime;     
    }

    void Update()
    {
        curTime += Time.deltaTime;
        if (Input.GetMouseButton(0))
        {
            if (curTime > coolTime)
            {
                GameObject bullet = Instantiate(bulletPrefab) as GameObject;
                bullet.transform.position = transform.position;
                bullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed * Time.deltaTime;
                Destroy(bullet, 3f);
                curTime = 0;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example_WeaponSystem : MonoBehaviour
{
    public GameObject weapon;
    public GameObject bulletPrefab1;
    public GameObject bulletPrefab2;
    public GameObject bulletPrefab3;
    public Transform firePosition;
    public float coolTime = 1f;
    public float curTime;
    public float bulletSpeed = 5000f;

    void Start()
    {
        weapon = bulletPrefab1;
        bulletPrefab1.tag = "weapon1";
        bulletPrefab2.tag = "weapon2";
        bulletPrefab3.tag = "weapon3";
        curTime = coolTime;
    }

    void Update()
    {        
        if(curTime <= coolTime)
        {
            curTime += Time.deltaTime;
        }

        if(Input.GetMouseButton(0))
        {
            BulletFire();            
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            WeaponSwap();
        }
    }

    public void BulletFire()
    {
        if (curTime >= coolTime)
        {
            GameObject bullet = Instantiate(weapon, firePosition.transform.position, firePosition.transform.rotation) as GameObject;
            Destroy(bullet, 1f);
            curTime = 0;
        }
    }

    public void WeaponSwap()
    {       
        switch (weapon.tag)
        {
            case "weapon1": 
                weapon = bulletPrefab2;
                break;
            case "weapon2":
                weapon = bulletPrefab3;
                break;
            case "weapon3":
                weapon = bulletPrefab1;
                break;
            default:
                break;
        }
    }
}

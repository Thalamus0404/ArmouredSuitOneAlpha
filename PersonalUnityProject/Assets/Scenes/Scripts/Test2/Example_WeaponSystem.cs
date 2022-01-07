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
    public Transform pointer;
    public float coolTime = 1f;
    public float curTime;
    public float bulletSpeed = 5000f;

    public bool isTrans = false;
    public int transGuage = 0;
    public int maxTransGuage = 1000;
    public int transGuageCost = 50;

    Ray ray;
    RaycastHit hit;
    public Example_Enemy example_Enemy;

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
        if (curTime <= coolTime)
        {
            curTime += Time.deltaTime;
        }

        if (Input.GetMouseButton(0))
        {
            if (isTrans)
            {
                TransBulletFire();
            }
            else
            {
                BulletFire();
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!isTrans)
            {
                WeaponSwap();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isTrans && transGuage > 0)
        {
            isTrans = true;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && isTrans)
        {
            isTrans = false;
        }
    }

    public void BulletFire()
    {
        if (curTime >= coolTime)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 dir = pointer.transform.position - firePosition.transform.position;
            dir.Normalize();
            GameObject bullet = Instantiate(weapon, firePosition.transform.position, Quaternion.LookRotation(dir)) as GameObject;
            bullet.tag = "PlayerBullet";
            Destroy(bullet, 1f);
            curTime = 0;
        }
    }

    public void TransBulletFire()
    {
        if (curTime >= coolTime && transGuage > 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);            
            transGuage -= transGuageCost;
            curTime = 0;
            if (hit.collider != null)
            {
                example_Enemy = hit.collider.GetComponent<Example_Enemy>();
                example_Enemy.hp -= 5;
            }            
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

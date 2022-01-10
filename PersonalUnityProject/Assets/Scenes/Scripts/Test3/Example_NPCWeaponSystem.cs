using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example_NPCWeaponSystem : MonoBehaviour
{
    public GameObject firePosition;
    public GameObject mainTarget;
    public GameObject weapon;
    public GameObject bulletPrefab1;
    public float coolTime = 1f;
    public float curTime;

    public AudioSource soundSource;
    public AudioClip fireSound;

    void Start()
    {
        weapon = bulletPrefab1;
        soundSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (curTime <= coolTime)
        {
            curTime += Time.deltaTime;
        }
    }

    public void NPCBulletFire()
    {
        if (curTime >= coolTime && mainTarget != null)
        {
            Vector3 dir = mainTarget.transform.position - firePosition.transform.position;
            dir.Normalize();
            GameObject bullet = Instantiate(weapon, firePosition.transform.position, Quaternion.LookRotation(dir)) as GameObject;
            soundSource.PlayOneShot(fireSound);
            if (gameObject.tag == "Ally")
            {
                bullet.tag = "AllyBullet";
            }
            else if (gameObject.tag == "Enemy")
            {
                bullet.tag = "EnemyBullet";
            }
            Destroy(bullet, 1f);
            curTime = 0;
        }
    }
}

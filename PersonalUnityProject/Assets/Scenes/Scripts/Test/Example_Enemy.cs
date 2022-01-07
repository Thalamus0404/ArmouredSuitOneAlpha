using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example_Enemy : MonoBehaviour
{
    public int hp = 5;
        
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "PlayerBullet":
                Destroy(other);
                hp -= other.GetComponent<Example_Bullet>().damage;
                Debug.Log(hp + "¸ÂÀ½");
                if (hp <= 0)
                {
                    GetComponentInParent<GameObject>().SetActive(false);
                }
                break;
            default:
                break;
        }
    }    
}

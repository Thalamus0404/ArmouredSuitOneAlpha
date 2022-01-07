using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exmaple_Radar : MonoBehaviour
{
    public List<GameObject> targets;    

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Player":
                if (gameObject.tag == "EnemyRadar")
                {
                    targets.Add(other.gameObject);
                }                
                break;
            case "Enemy":
                if (gameObject.tag == "PlayerRadar")
                {
                    targets.Add(other.gameObject);
                }
                //Debug.Log("충돌함");
                break;
            default:
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {     
        targets.Remove(other.gameObject);
        //Debug.Log("나감");
    }
}

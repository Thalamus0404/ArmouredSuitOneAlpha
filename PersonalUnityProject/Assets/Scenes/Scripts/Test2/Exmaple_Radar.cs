using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exmaple_Radar : MonoBehaviour
{
    public List<GameObject> targets;

    private void Update()
    {
        targets.RemoveAll(GameObject => GameObject != GameObject.activeInHierarchy);
    }

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
                if (gameObject.tag == "AllyRadar")
                {
                    targets.Add(other.gameObject);
                }
                //Debug.Log("충돌함");
                break;
            case "Ally":
                if (gameObject.tag == "EnemyRadar")
                {
                    targets.Add(other.gameObject);
                }
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

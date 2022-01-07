using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example_FrontArea : MonoBehaviour
{
    public bool isRanged = false;

    private void OnTriggerEnter(Collider other)
    {        
        if (gameObject.tag == "EnemyRadar")
        {
            if(other.gameObject.tag == "Player")
            {
                isRanged = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (gameObject.tag == "EnemyRadar")
        {
            if (other.gameObject.tag == "Player")
            {
                isRanged = false;
            }
        }
    }
}

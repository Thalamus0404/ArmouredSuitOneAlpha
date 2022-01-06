using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example_Enemy : MonoBehaviour
{
    public int hp = 5;

    void Update()
    {
        if (hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}

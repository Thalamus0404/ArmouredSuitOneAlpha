using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example_NPC_AI : MonoBehaviour
{
    public enum NPCSTATE
    {
        IDLE = 0,
        MOVE,
        SEARCH,
        CHASE,
        ATTACK,
        EVASION,
        DESTROY
    }

    public string[] objects = { "move", "protect", "overwatch", "attack" };

    NPCSTATE npcState;

    void Start()
    {
        npcState = GetComponent<NPCSTATE>();
    }

    void Update()
    {
        NPCState(objects[4]);
    }

    public void NPCState(string objects)
    {
        switch (npcState)
        {
            case NPCSTATE.IDLE:
                break;
            case NPCSTATE.MOVE:
                break;
            case NPCSTATE.SEARCH:
                break;
            case NPCSTATE.CHASE:
                break;
            case NPCSTATE.ATTACK:
                break;
            case NPCSTATE.EVASION:
                break;
            case NPCSTATE.DESTROY:
                break;
            default:
                break;
        }
    }
}

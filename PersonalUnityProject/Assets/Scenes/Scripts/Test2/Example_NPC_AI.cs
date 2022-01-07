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

    //public string[] objects = { "move", "protect", "overwatch", "attack" };
    [SerializeField]
    public NPCSTATE npcState;

    public float npcSpeed = 10f;
    public float npcSpeedX;
    public float npcRotSpeed = 0.1f;
    public float acceleration = 0.1f;

    public GameObject mainTarget;

    public Exmaple_Radar radar;
    public Example_NPCWeaponSystem weaponSystem;
    public Example_FrontArea frontArea;

    public float weaponRange;
        
    public float stateCurTime;
    public float stateCoolTime = 5f;
    public float attackCurTime;
    public float attackCoolTime = 5f;
    public float distance;

    RaycastHit hit;

    void Start()
    {
        radar = GetComponentInChildren<Exmaple_Radar>();
        weaponSystem = GetComponent<Example_NPCWeaponSystem>();
        frontArea = GetComponentInChildren<Example_FrontArea>();
    }

    void Update()
    {
        NPCMove(npcSpeedX);
        NPCLogic();
        //RaySensor();
    }

    public void NPCLogic() // string[] objects[]을 임무로 넣을 예정
    {       

        if (mainTarget != null)
        {
            distance = Vector3.Distance(transform.position, mainTarget.transform.position);
        }
        switch (npcState)
        {
            case NPCSTATE.IDLE:
                npcState = NPCSTATE.MOVE;
                break;
            case NPCSTATE.MOVE:
                if (npcSpeedX <= npcSpeed)
                {
                    npcSpeedX += acceleration * npcSpeed;
                }
                else
                {
                    npcSpeedX = npcSpeed;
                }
                if (radar.targets.Count > 0 && radar.targets[0] != null)
                {
                    stateCurTime += Time.deltaTime;
                    if (stateCurTime > stateCoolTime)
                    {
                        stateCurTime = 0;
                        mainTarget = radar.targets[0];
                        npcState = NPCSTATE.SEARCH;
                    }
                }
                break;                
            case NPCSTATE.SEARCH:
                npcState = NPCSTATE.CHASE;
                break;
            case NPCSTATE.CHASE:
                Vector3 dir = mainTarget.transform.position - transform.position;
                Debug.DrawLine(mainTarget.transform.position, transform.position, Color.red);                    
                dir.Normalize();                    
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), npcRotSpeed * Time.deltaTime);
                if(distance < weaponRange && frontArea.isRanged)
                {
                    stateCurTime += Time.deltaTime;
                    if (stateCurTime > stateCoolTime)
                    {
                        npcState = NPCSTATE.ATTACK;
                        stateCurTime = 0;
                    }
                }
                else if(distance < weaponRange && !frontArea.isRanged)
                {
                    if (npcSpeedX >= 0.5f * npcSpeed)
                    {
                        npcSpeedX += - 0.5f * acceleration * npcSpeed;
                    }
                    else
                    {
                        npcSpeedX = 0.5f * npcSpeed;
                    }
                }
                else
                {
                    npcState = NPCSTATE.MOVE;
                }
                break;
            case NPCSTATE.ATTACK:
                weaponSystem.mainTarget = mainTarget;                
                weaponSystem.NPCBulletFire();
                attackCurTime += Time.deltaTime;                
                if (attackCurTime > attackCoolTime || distance > weaponRange)
                {                    
                    attackCurTime = 0;
                    npcState = NPCSTATE.MOVE;
                }                
                break;
            case NPCSTATE.EVASION:
                break;
            case NPCSTATE.DESTROY:
                break;
            default:
                break;
        }
    }

    public void NPCMove(float speed)
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }    

    //public void RaySensor()
    //{
    //    Physics.Raycast(transform.position, Vector3.forward, out hit);
    //    if (hit.collider != null)
    //    {
    //        if (gameObject.tag == "Ally")
    //        {
    //            if (hit.collider.tag == "Enemy")
    //            {
    //                isRanged = true;
    //            }
    //            else
    //            {
    //                isRanged = false;
    //            }
    //        }
    //        if (gameObject.tag == "Enemy")
    //        {
    //            if (hit.collider.tag == "Player" || hit.collider.tag == "Ally")
    //            {
    //                isRanged = true;
    //            }
    //            else
    //            {
    //                isRanged = false;
    //            }
    //        }
    //    }
    //}
}

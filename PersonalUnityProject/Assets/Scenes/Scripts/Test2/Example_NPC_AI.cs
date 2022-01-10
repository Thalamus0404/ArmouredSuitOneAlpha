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
    public float deathTime = 3f;

    RaycastHit hit;

    public Vector3 evasionDirection;
    public Vector3 moveDirection;

    public bool isCrashed = false;
    public bool isDead = false;

    public float curTime;
    public float coolTime = 1f;

    public float chaseCurTime;
    public float chaseCoolTime = 10f;
    public float evasionCurTime;
    public float evasionCoolTime = 10f;

    public GameObject point;
    public GameObject deathEffect;
    public GameObject fireEffect;
    public GameObject hitEffect;

    void Start()
    {
        moveDirection = Vector3.forward;
        radar = GetComponentInChildren<Exmaple_Radar>();
        weaponSystem = GetComponent<Example_NPCWeaponSystem>();
        frontArea = GetComponentInChildren<Example_FrontArea>();        
    }

    void Update()
    {
        if (isCrashed)
        {
            curTime += Time.deltaTime;
            if (npcSpeedX >= 0)
            {
                npcSpeedX += - acceleration * npcSpeed;
            }
            if (curTime > coolTime)
            {
                curTime = 0;
                isCrashed = false;
                moveDirection = Vector3.forward;
            }
        }
        NPCMove(npcSpeedX);
        if (!isCrashed)
        {
            NPCLogic();
        }
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
                mainTarget = null;
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
                //else
                //{
                //    Vector3 dirP = point.transform.position - transform.position;
                //    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dirP), npcRotSpeed * Time.deltaTime);
                //}
                break;                
            case NPCSTATE.SEARCH:
                npcState = NPCSTATE.CHASE;
                break;
            case NPCSTATE.CHASE:
                if (mainTarget != null)
                {
                    Vector3 dir = mainTarget.transform.position - transform.position;
                    Debug.DrawLine(mainTarget.transform.position, transform.position, Color.red);
                    dir.Normalize();
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), npcRotSpeed * Time.deltaTime);
                    if (distance < weaponRange && frontArea.isRanged)
                    {
                        if (npcSpeedX <= npcSpeed)
                        {
                            npcSpeedX += acceleration * npcSpeed;
                        }
                        else
                        {
                            npcSpeedX = npcSpeed;
                        }
                        attackCurTime += Time.deltaTime;
                        if (attackCurTime > attackCoolTime)
                        {
                            npcState = NPCSTATE.ATTACK;
                            attackCurTime = 0;
                        }
                    }
                    else if (distance < weaponRange && !frontArea.isRanged)
                    {
                        chaseCurTime += Time.deltaTime;
                        if (chaseCurTime < chaseCoolTime)
                        {
                            
                        }
                        else
                        {
                            chaseCurTime = 0;
                            npcState = NPCSTATE.EVASION;
                        }
                    }
                    else
                    {
                        npcState = NPCSTATE.IDLE;
                    }
                }
                else
                {
                    npcState = NPCSTATE.IDLE;
                }
                break;
            case NPCSTATE.ATTACK:
                weaponSystem.mainTarget = mainTarget;                
                weaponSystem.NPCBulletFire();
                attackCurTime += Time.deltaTime;                
                if (attackCurTime > attackCoolTime || distance > weaponRange)
                {                    
                    attackCurTime = 0;
                    npcState = NPCSTATE.IDLE;
                }                
                break;
            case NPCSTATE.EVASION:
                evasionCurTime += Time.deltaTime;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(evasionDirection), npcRotSpeed * Time.deltaTime);
                if (evasionCurTime > evasionCoolTime)
                {
                    evasionCurTime = 0;
                    npcState = NPCSTATE.IDLE;                    
                }
                break;
            case NPCSTATE.DESTROY:
                //if (npcSpeedX <= npcSpeed)
                //{
                //    npcSpeedX += acceleration * npcSpeed;
                //}
                //else
                //{
                    npcSpeedX = npcSpeed;
                //}
                if (!isDead)
                {                    
                    StartCoroutine("NPCDie");
                    isDead = true;
                }
                break;
            default:
                break;
        }
    }

    public void NPCMove(float speed)
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }
    
    IEnumerator NPCDie()
    {
        fireEffect.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(deathTime);
        GameObject flash = Instantiate(deathEffect, transform.position, transform.rotation);
        Destroy(flash, 3f);
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "PlayerBullet" || collision.gameObject.tag != "EnemyBullet" || collision.gameObject.tag != "AllyBullet")
        {
            Vector3 dir = collision.transform.position - transform.position;
            moveDirection = Vector3.Reflect(dir, collision.contacts[0].normal);
            isCrashed = true;
            //Debug.Log("충돌함");
        }
    }
}

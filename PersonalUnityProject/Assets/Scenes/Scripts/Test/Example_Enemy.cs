using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example_Enemy : MonoBehaviour
{
    public int hp = 5;
    public Example_NPC_AI example_NPC_AI;
    public Vector3 rndDirection;
    public Exmaple_Radar radar;
    public GameObject hitEffect;

    public AudioSource soundSource;
    public AudioClip hitSound;
    public AudioClip deathSound;

    void Start()
    {
        example_NPC_AI = GetComponentInParent<Example_NPC_AI>();
        hitEffect = example_NPC_AI.hitEffect;
        StartCoroutine("MakeRndDirection");
        soundSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "PlayerBullet":
                Destroy(other);
                if (gameObject.tag == "Enemy")
                {
                    hitEffect.GetComponent<ParticleSystem>().Play();
                    soundSource.PlayOneShot(hitSound);
                    hp -= other.GetComponent<Example_Bullet>().damage;
                    StartCoroutine("MakeRndDirection");
                    example_NPC_AI.stateCurTime = 0;
                    example_NPC_AI.npcState = Example_NPC_AI.NPCSTATE.EVASION;
                    Debug.Log(name + hp + "맞음");
                }
                break;
            case "EnemyBullet":
                Destroy(other);
                if (gameObject.tag == "Ally")
                {
                    hitEffect.GetComponent<ParticleSystem>().Play();
                    soundSource.PlayOneShot(hitSound);
                    hp -= other.GetComponent<Example_Bullet>().damage;
                    StartCoroutine("MakeRndDirection");
                    example_NPC_AI.stateCurTime = 0;
                    example_NPC_AI.npcState = Example_NPC_AI.NPCSTATE.EVASION;
                    Debug.Log(name + hp + "맞음");
                }
                break;
            case "AllyBullet":
                Destroy(other);
                if (gameObject.tag == "Enemy")
                {
                    hitEffect.GetComponent<ParticleSystem>().Play();
                    soundSource.PlayOneShot(hitSound);
                    hp -= other.GetComponent<Example_Bullet>().damage;
                    StartCoroutine("MakeRndDirection");
                    example_NPC_AI.stateCurTime = 0;
                    example_NPC_AI.npcState = Example_NPC_AI.NPCSTATE.EVASION;
                    Debug.Log(name + hp + "맞음");
                }
                break;
            default:                
                break;
        }
        if (hp <= 0 && !example_NPC_AI.isDead)
        {
            soundSource.PlayOneShot(deathSound);
            example_NPC_AI.npcState = Example_NPC_AI.NPCSTATE.DESTROY;            
        }
    }

    IEnumerator MakeRndDirection()
    {
        int rnd = Random.Range(0, 3);
        switch (rnd)
        {
            case 0:
                rndDirection = transform.up;
                break;
            case 1:
                rndDirection = -transform.up;
                break;
            case 2:
                rndDirection = transform.right;
                break;
            case 3:
                rndDirection = -transform.right;
                break;
            default:
                break;
        }
        example_NPC_AI.evasionDirection = rndDirection;
        yield return new WaitForEndOfFrame();
    }
}

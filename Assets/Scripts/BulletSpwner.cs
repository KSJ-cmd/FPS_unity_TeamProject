using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BulletSpwner : MonoBehaviour
{
    public GameObject bulletPerfab;
    public float spawnRateMin = 0.5f;
    public float spawnRateMax = 2f;

    private Transform target;
    private float spawnRata;
    private float timeAfterSpawn;

    private float idleDelayTime = 3f;
    
    public int hp = 100;
    int minHp = 100;
    public HPBar hpbar;
    public GameObject level;

    public bool isMoving = false;
    private NavMeshAgent nvAgent;
    Animator animator;

    //effect
    public ParticleSystem Boom;

    //

    //FSM
    enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Die,
    }
    EnemyState enemyState = EnemyState.Idle;

    // Start is called before the first frame update
    void Start()
    {
        timeAfterSpawn = 0f;

        spawnRata = Random.Range(spawnRateMin, spawnRateMax);

        target = FindObjectOfType<PlayerController>().transform;

       
        nvAgent = GetComponent<NavMeshAgent>();

        animator = GetComponent<Animator>();
        int hpLevel = (int)Mathf.Sqrt(GameManager.instance.levelTime);
        hp = minHp + (10 * hpLevel);
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            return;
        }
        float distance = Vector3.Distance(gameObject.transform.position, target.position);
        if (distance < 15f)
        {
            enemyState = EnemyState.Attack;
            isMoving = false;
        }
        else
        {
            if (isMoving == false)
            {
                enemyState = EnemyState.Idle;
            }
            else
            {
                enemyState = EnemyState.Move;
            }
        }
        timeAfterSpawn += Time.deltaTime;
        switch (enemyState)
        {
            case EnemyState.Idle:
                {
                    Idle();
                }
                break;
            case EnemyState.Move:
                {
                    isMoving = true;
                    Move();
                }
                break;
            case EnemyState.Attack:
                {
                    Attack();
                }
                break;
            case EnemyState.Die:
                {  
                    //Die();
                }
                break;
        }
    }
    void Idle()
    {
        if (timeAfterSpawn > idleDelayTime)
        {
            enemyState = EnemyState.Move;
            isMoving = true;
            timeAfterSpawn = 0f;
        }
    }
    void Move()
    {
        StartCoroutine(MonsterAI());
    }

    void Attack()
    {
        if (timeAfterSpawn >= spawnRata)
        {

            timeAfterSpawn = 0f;
            GameObject bulletSpawn = transform.Find("BulletSpawn").gameObject;
            transform.LookAt(target);
            GameObject bullet = Instantiate(bulletPerfab, bulletSpawn.transform.position, transform.rotation);
            bullet.transform.forward = transform.forward;

            //타켓팅 (헤드) 
            //Transform head = target.Find("head1").transform;

            spawnRata = Random.Range(spawnRateMin, spawnRateMax);
        }
    }

    public void  GetDamage(int damage)
    {
        hp -= damage;
        hpbar.SetHP(hp);
        if (hp <= 0)
        {
            enemyState = EnemyState.Die;
            isMoving = false;
            
            Die();
        }
    }
    void Die()
    {
        Debug.Log("Die");
        //gameObject.SetActive(false);
        ItemManager.instance.ItemCreate(transform.position);
        SoundManager.instance.ItemDropSound();

        Instantiate<ParticleSystem>(Boom, transform.position, Quaternion.identity);
        SoundManager.instance.EnemyDieSound();

        GetComponent<CapsuleCollider>().enabled = false;
        animator.SetTrigger("Die");

        GameManager.instance.DieBulletSpawner(gameObject);
        Destroy(gameObject, 5f);
        
    }

    IEnumerator MonsterAI()
    {
        while (hp > 0 && enemyState == EnemyState.Move)
        {
            yield return new WaitForSeconds(0.2f);
            if (isMoving)
            {
                nvAgent.destination = target.position;
                nvAgent.isStopped = false;
                animator.SetBool("isMoving", true);
            }
            else
            {
                nvAgent.isStopped = true;
                animator.SetBool("isMoving", false);
            }
        }
    }
}

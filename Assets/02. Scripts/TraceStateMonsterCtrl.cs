using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class TraceStateMonsterCtrl : MonoBehaviour
{
    public enum MonsterState { idle, trace, attack, die };
    public MonsterState monsterState = MonsterState.idle;
    private Transform monsterTr;
    private Transform playerTr;
    public NavMeshAgent nvAgent;
    public Animator animator;

    //public float traceDist = 5.0f;
    public float attackDist = 2.0f;
    public bool isDie = false;
    private bool isTrace = false;//trace가 on 되면 무조건 플레이어를 따라간다.

    public int hp = 100;

    //private GameUI gameUI;
    // Start is called before the first frame update
    void Start()
    {
        monsterTr = GetComponent<Transform>();
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        nvAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        //nvAgent.destination = playerTr.position;
        //gameUI = GameObject.Find("GameUI").GetComponent<GameUI>();
        StartCoroutine(this.CheckMonsterState());
        StartCoroutine(this.MonsterAction());
    }

    public IEnumerator CheckMonsterState()
    {
        while (!isDie)
        {
            yield return new WaitForSeconds(0.2f);

            float dist = Vector3.Distance(playerTr.position, monsterTr.position);

            if (monsterState == MonsterState.die)
            {
                Debug.Log("CheckMonsterState(): die");
            }
            else if (dist <= attackDist)
            {
                monsterState = MonsterState.attack;
                Debug.Log("CheckMonsterState(): attack!");
            }
            else
            {
                monsterState = MonsterState.trace;
                Debug.Log("CheckMonsterState(): trace!");
            }
        }
    }

    public IEnumerator MonsterAction()
    {
        while (!isDie)
        {
            switch (monsterState)
            {
                case MonsterState.idle:
                    nvAgent.isStopped = true;
                    animator.SetBool("IsTrace", false);

                    break;
                case MonsterState.trace:
                    nvAgent.destination = playerTr.position;
                    nvAgent.isStopped = false;
                    animator.SetBool("IsAttack", false);
                    animator.SetBool("IsTrace", true);

                    break;
                case MonsterState.attack:
                    nvAgent.isStopped = true;
                    animator.SetBool("IsAttack", true);
                    break;
            }
            yield return null;
        }
    }

    void OnPlayerDie()
    {
        StopAllCoroutines();
        nvAgent.isStopped = true;
        animator.SetTrigger("IsPlayerDie");
    }


    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Bullet")
{
            hp -= coll.gameObject.GetComponent<PlayerBullet>().damage;
            //hp -= 10;
            if (hp <= 0)
            {
                MonsterDie();
            }
            else
            {
                animator.SetTrigger("IsHit");
            }
            Destroy(coll.gameObject);

        }
    }

    void MonsterDie()
    {
        StopAllCoroutines();
        isDie = true;
        monsterState = MonsterState.die;
        nvAgent.isStopped = true;
        animator.SetTrigger("IsDie");

        gameObject.GetComponentInChildren<CapsuleCollider>().enabled = false;

        foreach (Collider coll in gameObject.GetComponentsInChildren<SphereCollider>())
        {
            coll.enabled = false;
        }
        //gameUI.DispSocre(50);
    }
    public void ReturnObject()
    {
        MonsterPool.ReturnObject(this);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive)
        {
            isActive = false;
            for(int i=0;i<10;i++)
            {
                var monster = MonsterPool.GetObject();
                monster.transform.position = this.transform.position;
                if (monster.isDie == true)
                {
                    monster.hp = 100;
                    monster.isDie = false;
                    monster.monsterState = TraceStateMonsterCtrl.MonsterState.trace;
                    monster.nvAgent.isStopped = false;

                    monster.GetComponentInChildren<CapsuleCollider>().enabled = true;

                    foreach (Collider coll in monster.GetComponentsInChildren<SphereCollider>())
                    {
                        coll.enabled = true;
                    }
                    StartCoroutine(monster.CheckMonsterState());
                    StartCoroutine(monster.MonsterAction());
                    
                }
                
            }
        }
        
    }
}

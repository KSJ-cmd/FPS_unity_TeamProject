using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPool : MonoBehaviour
{
    public static MonsterPool Instance;
    [SerializeField]
    private GameObject poolingObjectPrefab;

    Queue<TraceStateMonsterCtrl> poolingObjectQueue = new Queue<TraceStateMonsterCtrl>();
    private void Awake() { Instance = this; Initialize(10); }
    private void Initialize(int initCount)
    { 
        for (int i = 0; i < initCount; i++) 
        { 
            poolingObjectQueue.Enqueue(CreateNewObject()); 
        } 
    }
    private TraceStateMonsterCtrl CreateNewObject() 
    { 
        var newObj = Instantiate(poolingObjectPrefab).GetComponent<TraceStateMonsterCtrl>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj; 
    }
    public static TraceStateMonsterCtrl GetObject() 
    {
        if (Instance.poolingObjectQueue.Count > 0) 
        {
            var obj = Instance.poolingObjectQueue.Dequeue();
            obj.transform.SetParent(null); obj.gameObject.SetActive(true);
            return obj;
        } 
        else 
        {
            var newObj = Instance.CreateNewObject();
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(null);
            return newObj;
        } 
    }
    public static void ReturnObject(TraceStateMonsterCtrl obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);
        Instance.poolingObjectQueue.Enqueue(obj);
    }

}

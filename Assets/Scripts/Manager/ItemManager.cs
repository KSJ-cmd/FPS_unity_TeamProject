using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{

    private static ItemManager _instance = null;
    public static ItemManager instance { get { return _instance; } }

    private float surviveTime;
    int prevEventTime;
    enum ItemType
    {
        HEAL,
        HG,
        AR,
        SR
    }

    public GameObject level;

    public GameObject healItemPrefab;
    public GameObject hgItemPrefab;
    public GameObject arItemPrefab;
    public GameObject srItemPrefab;

    List<GameObject> healItemList = new List<GameObject>();
    List<GameObject> hgItemList = new List<GameObject>();
    List<GameObject> arItemList = new List<GameObject>();
    List<GameObject> srItemList = new List<GameObject>();

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (_instance != null)
            {
                Destroy(this.gameObject);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        surviveTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        surviveTime += Time.deltaTime;
        int eventTime = (int)(surviveTime % 30f);
        if (eventTime == 0 & prevEventTime != eventTime)
        {
            foreach (GameObject item in healItemList)
            {
                Destroy(item);
            }
            healItemList.Clear();
            foreach (GameObject item in hgItemList)
            {
                Destroy(item);
            }
            hgItemList.Clear();
            foreach (GameObject item in arItemList)
            {
                Destroy(item);
            }
            arItemList.Clear();
            foreach (GameObject item in srItemList)
            {
                Destroy(item);
            }
            srItemList.Clear();

        }
        prevEventTime = eventTime;
    }
    public void ItemCreate(Vector3 enemyPosition)
    {
        int randomCreateAmount = Random.Range(2, 6);
        for (int i = 0; i < randomCreateAmount; i++)
        {
            int createItemType = Random.Range(0, 4);

            Vector3 createPosition = new Vector3(enemyPosition.x + Random.Range(-2.0f, 3.0f), enemyPosition.y, enemyPosition.z + Random.Range(-2.0f, 3.0f));
            switch ((ItemType)createItemType)
            {
                case ItemType.HEAL:
                    {
                        GameObject HealItem = Instantiate(healItemPrefab, createPosition, Quaternion.identity);
                        HealItem.transform.parent = level.transform;
                        healItemList.Add(HealItem);
                    }
                    break;
                case ItemType.HG:
                    {
                        GameObject HGItem = Instantiate(hgItemPrefab, createPosition, Quaternion.identity);
                        HGItem.transform.parent = level.transform;
                        hgItemList.Add(HGItem);
                    }
                    break;
                case ItemType.AR:
                    {
                        GameObject ARItem = Instantiate(arItemPrefab, createPosition, Quaternion.identity);
                        ARItem.transform.parent = level.transform;
                        arItemList.Add(ARItem);
                    }
                    break;
                case ItemType.SR:
                    {
                        GameObject SRItem = Instantiate(srItemPrefab, createPosition, Quaternion.identity);
                        SRItem.transform.parent = level.transform;
                        srItemList.Add(SRItem);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}

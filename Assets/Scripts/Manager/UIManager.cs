using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    private static UIManager _instance = null;
    public static UIManager instance { get { return _instance; } }

    public Canvas UIcanvas;
    public Image HGUI;
    public Image ARUI;
    public Image SRUI;
    public GameObject LootingHGUI;
    public GameObject LootingARUI;
    public GameObject LootingSRUI;

    public List<GameObject> LootingItem = new List<GameObject>();
    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if(_instance != null)
            {
                Destroy(this.gameObject);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {

        
        GetMagazinePrint();
    }
    public void CreateLootingUI(string name, int amount)
    {
       
        switch (name)
        {
            case "HGBullet(Clone)":
                {
                    GameObject LootingUI = Instantiate(LootingHGUI, LootingHGUI.transform.position, Quaternion.identity);
                    LootingUI.transform.SetParent(UIcanvas.transform);
                    TextMeshProUGUI Amount = LootingUI.transform.Find("LootingHGAmount").GetComponent<TextMeshProUGUI>();
                    Amount.text = "+" + amount;
                    LootingItem.Add(LootingUI);
                }
                break;
            case "ARBullet(Clone)":
                {
                    GameObject LootingUI = Instantiate(LootingARUI, LootingHGUI.transform.position, Quaternion.identity);
                    LootingUI.transform.SetParent(UIcanvas.transform);
                    TextMeshProUGUI Amount = LootingUI.transform.Find("LootingARAmount").GetComponent<TextMeshProUGUI>();
                    Amount.text = "+" + amount;
                    LootingItem.Add(LootingUI);
                }
                break;
            case "SRBullet(Clone)":
                {
                    GameObject LootingUI = Instantiate(LootingSRUI, LootingHGUI.transform.position, Quaternion.identity);
                    LootingUI.transform.SetParent(UIcanvas.transform);
                    TextMeshProUGUI Amount = LootingUI.transform.Find("LootingSRAmount").GetComponent<TextMeshProUGUI>();
                    Amount.text = "+" + amount;
                    LootingItem.Add(LootingUI);
                }
                break;
            default:
                break;
        }
        
    }
    // Update is called once per frame
    void Update()
    {
       

    }
   
    public void WeaponIconPrint()
    {
        switch (WeaponManager.instance.weaponType)
        {
            case WeaponManager.WeaponType.HG:
                {
                    HGUI.gameObject.SetActive(true);
                    ARUI.gameObject.SetActive(false);
                    SRUI.gameObject.SetActive(false);
                }
                break;
            case WeaponManager.WeaponType.AR:
                {
                    HGUI.gameObject.SetActive(false);
                    ARUI.gameObject.SetActive(true);
                    SRUI.gameObject.SetActive(false);
                }
                break;
            case WeaponManager.WeaponType.SR:
                {
                    HGUI.gameObject.SetActive(false);
                    ARUI.gameObject.SetActive(false);
                    SRUI.gameObject.SetActive(true);
                }
                break;
        }
    }
    public void GetMagazinePrint()
    {
        int index = 0;
        foreach(GameObject Looting in LootingItem)
        {
            RectTransform rect = (RectTransform)Looting.transform;
            rect.anchoredPosition = new Vector2(-174, -185 + (index * 40));
            index++;
            Destroy(Looting, 0.5f);
        }
        LootingItem.Clear();
        Invoke("GetMagazinePrint", 0.5f);
    }
}

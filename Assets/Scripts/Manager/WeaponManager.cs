using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    private static WeaponManager _instance = null;

    public static WeaponManager instance { get { return _instance; } }

    public enum WeaponType {HG,AR,SR};

    public WeaponType weaponType;

    public int maxMagazine
    {
        get;
        private set;
    }
    public int currentMagazine
    {
        get;
        set;
    }
    public int haveMagazine
    {
        get;
        private set;
    }
    public int HGsaveMagazine
    {
        get;
        private set;
    }
    public int ARsaveMagazine
    {
        get;
        private set;
    }
    public int SRsaveMagazine
    {
        get;
        private set;
    }
    public int HGsaveCurMagazine
    {
        get;
        private set;
    }
    public int ARsaveCurMagazine
    {
        get;
        private set;
    }
    public int SRsaveCurMagazine
    {
        get;
        private set;
    }
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
        HGsaveMagazine = 300;
        ARsaveMagazine = 200;
        SRsaveMagazine = 40;
        weaponType = WeaponType.AR;
        LoadMagazine();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void AR()
    {
        maxMagazine = 50;
        int magazineGap = maxMagazine - currentMagazine;
        if (haveMagazine > magazineGap)
        {
            currentMagazine += Mathf.Clamp(haveMagazine, 0, magazineGap);
            haveMagazine -= magazineGap;
        }
        else
        {
            currentMagazine += haveMagazine;
            haveMagazine = 0;
        }
    }
    void SR()
    {
        maxMagazine = 5;
        int magazineGap = maxMagazine - currentMagazine;
        if (haveMagazine > magazineGap)
        {
            currentMagazine += Mathf.Clamp(haveMagazine, 0, magazineGap);
            haveMagazine -= magazineGap;
        }
        else
        {
            currentMagazine += haveMagazine;
            haveMagazine = 0;
        }
    }
    void HG()
    {
        maxMagazine = 20;
        int magazineGap = maxMagazine - currentMagazine;
        if (haveMagazine > magazineGap)
        {
            currentMagazine += Mathf.Clamp(haveMagazine, 0, magazineGap);
            haveMagazine -= magazineGap;
        }
        else
        {
            currentMagazine += haveMagazine;
            haveMagazine = 0;
        }
    }
    public void Reload()
    {
        switch (weaponType)
        {
            case WeaponType.HG:
                {
                    HG();
                }
                break;
            case WeaponType.AR:
                {
                    AR();
                }
                break;
            case WeaponType.SR:
                {
                    SR();
                }
                break;
            default:
                break;
        }
    }
    //총의 현재,보유 탄창 Save
    void SaveAR()
    {
        ARsaveMagazine = haveMagazine;
        ARsaveCurMagazine = currentMagazine;
    }
    void SaveSR()
    {
        SRsaveMagazine = haveMagazine;
        SRsaveCurMagazine = currentMagazine;
    }
    void SaveHG()
    {
        HGsaveMagazine = haveMagazine;
        HGsaveCurMagazine = currentMagazine;
    }
    public void SaveMagazine()
    {
        switch (weaponType)
        {
            case WeaponType.HG:
                {
                    SaveHG();
                }
                break;
            case WeaponType.AR:
                {
                    SaveAR();
                }
                break;
            case WeaponType.SR:
                {
                    SaveSR();
                }
                break;
            default:
                break;
        }
    }
    //총의 현재,보유 탄창 Load
    void LoadAR()
    {
        haveMagazine = ARsaveMagazine;
        currentMagazine = ARsaveCurMagazine;
    }
    void LoadSR()
    {
        haveMagazine = SRsaveMagazine;
        currentMagazine = SRsaveCurMagazine;
    }
    void LoadHG()
    {
        haveMagazine = HGsaveMagazine;
        currentMagazine = HGsaveCurMagazine;
    }
    public void LoadMagazine()
    {
        switch (weaponType)
        {
            case WeaponType.HG:
                {
                    LoadHG();
                }
                break;
            case WeaponType.AR:
                {
                    LoadAR();
                }
                break;
            case WeaponType.SR:
                {
                    LoadSR();
                }
                break;
            default:
                break;
        }
    }
    public void GetMagazine(string name)
    {
        if(name.Contains("HG"))
        {
            name = "HGBullet(Clone)";
        }
        else if (name.Contains("AR"))
        {
            name = "ARBullet(Clone)";
        }
        else if (name.Contains("SR"))
        {
            name = "SRBullet(Clone)";
        }
        switch (name)
        {
            case "HGBullet(Clone)":
                {
                    if (weaponType == WeaponType.HG)
                    {
                        int randomAmount = Random.Range(2, 9);
                        haveMagazine += randomAmount;
                        UIManager.instance.CreateLootingUI(name, randomAmount);
                    }
                    else
                    {
                        int randomAmount = Random.Range(2, 9);
                        HGsaveMagazine += randomAmount;
                        UIManager.instance.CreateLootingUI(name, randomAmount);
                    }
                }
                break;
            case "ARBullet(Clone)":
                {
                    if (weaponType == WeaponType.AR)
                    {
                        int randomAmount = Random.Range(10, 21);
                        haveMagazine += randomAmount;
                        UIManager.instance.CreateLootingUI(name, randomAmount);
                    }
                    else
                    {
                        int randomAmount = Random.Range(10, 21);
                        ARsaveMagazine += randomAmount;
                        UIManager.instance.CreateLootingUI(name, randomAmount);
                    }
                }
                break;
            case "SRBullet(Clone)":
                {
                    if (weaponType == WeaponType.SR)
                    {
                        int randomAmount = Random.Range(1, 4);
                        haveMagazine += randomAmount;
                        UIManager.instance.CreateLootingUI(name, randomAmount);
                    }
                    else
                    {
                        int randomAmount = Random.Range(1, 4);
                        SRsaveMagazine += randomAmount;
                        UIManager.instance.CreateLootingUI(name, randomAmount);
                    }
                }
                break;
            default:
                break;
        }
    }

}

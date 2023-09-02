using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HPBar : MonoBehaviour
{
    public Image hpbar;
    

    public void SetHP(int hp)
    {
        hpbar.fillAmount = (float)hp / 150;
    }
}

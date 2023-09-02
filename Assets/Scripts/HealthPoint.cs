using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthPoint : MonoBehaviour
{
    public TextMeshProUGUI healthPoint;
    // Start is called before the first frame update

    public void setHealthPoint(int hp)
    {
        healthPoint.text = "" + hp;
    }
}

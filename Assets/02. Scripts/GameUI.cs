using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Text txtScore;
    private int totScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("TOT_SCORE", 0);
        totScore = PlayerPrefs.GetInt("TOT_SCORE", 0);
        DispSocre(0);    
    }

    public void DispSocre(int score)
    {
        totScore += score;
        txtScore.text = "score <color=#ff0000>" + totScore.ToString() + "</color>";

        PlayerPrefs.SetInt("TOT_SCORE", totScore);
    }

 
}

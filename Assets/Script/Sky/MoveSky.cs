using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSky : MonoBehaviour
{
    float degree;

    private void Start()
    {
        degree = 0;
    }

    private void Update()
    {
        degree += Time.deltaTime;
        if(degree >= 360)
        {
            degree = 0;
        }

        RenderSettings.skybox.SetFloat("_Rotation", degree);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomlight : MonoBehaviour
{

    public Light light;
    int random;
    float random2;
    float deltaTime = 0;
    private void Start()
    {

    }

    private void Update()
    {
        light = GetComponent<Light>();
        ExplodeAfter();
        random2 = Random.Range(0.1f, 1f);
    }

    void ExplodeAfter()
    {
        deltaTime += Time.deltaTime;
        if (deltaTime >= random2)
        {
            random = Random.Range(5, 10);
            light.range = random;
            deltaTime = 0;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorReverse : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotationSpeed = 60f;
    public GameObject level;
    // Update is called once per frame
    void Update()
    {
        rotationSpeed = (-1)*level.GetComponent<Rotator>().rotationSpeed;
        transform.Rotate(0f, rotationSpeed*Time.deltaTime, 0f);
    }
}

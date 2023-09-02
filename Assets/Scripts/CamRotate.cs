using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    public float rotSpeed = 120;

    //마우스 조작 각도
    float mx;
    float my;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");

        //Vector3 dir = new Vector3(-v, h, 0);
        //Vector3 angle = transform.eulerAngles;
        //angle += dir * rotSpeed * Time.deltaTime;

        mx += h * rotSpeed * Time.smoothDeltaTime;
        my += v * rotSpeed * Time.smoothDeltaTime;

        my = Mathf.Clamp(my, -80, 80);

        transform.eulerAngles = new Vector3(-my, mx, 0);
    }
    //private void LookAround()
    //{
    //    Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")); 
    //    Vector3 camAngle = cameraArm.rotation.eulerAngles; 
    //    cameraArm.rotation = Quaternion.Euler(camAngle.x - mouseDelta.y, camAngle.y + mouseDelta.x, camAngle.z);
    //}

}

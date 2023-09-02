using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    
    public GameObject door_Right;
    public GameObject door_Left;
    // Start is called before the first frame update
    void Start()
    {
        //door_Right = gameObject.transform.GetChild(0).gameObject;
        //door_Left = gameObject.transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDoor()
    {

    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("door col player");

            if (other.GetComponent<PlayerController>().keyAmount > 0)
            {
                other.GetComponent<PlayerController>().UseKey();
                door_Right.transform.Rotate(new Vector3(0, 0, 90));
                door_Left.transform.Rotate(new Vector3(0, 0, -90));
                this.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }
}

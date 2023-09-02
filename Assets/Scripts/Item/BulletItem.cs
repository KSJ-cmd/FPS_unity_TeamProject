using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletItem : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            SoundManager.instance.ItemGetSound();
            WeaponManager.instance.GetMagazine(name);
            //Debug.Log(name);
            //획득 사운드;
            Destroy(gameObject);
        }
    }
}

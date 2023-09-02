using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 50f;
    private Rigidbody bulletRigidbody;

    public int damage = 30;
    bool isSound = false;
    // Start is called before the first frame update
    void Start()
    { 
        bulletRigidbody = GetComponent<Rigidbody>();


        bulletRigidbody.AddForce(transform.forward * speed);    

        Destroy(gameObject,5f);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerController playerController = other.GetComponent<PlayerController>();


            if (playerController != null && playerController.hp > 0)
            {

                playerController.GetDamage(damage);
                Destroy(gameObject);
                //playerController.Die();
            }
        }
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        var target = FindObjectOfType<PlayerController>();
        if (target != null)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance < 10f && isSound == false)
            {
                isSound = true;
                SoundManager.instance.BulletMoveSound();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 8f;
    private Rigidbody bulletRigidbody;
    public int damage = 30;

    public ParticleSystem enemyEffect;
    public ParticleSystem hitEffect;
    // Start is called before the first frame update
    void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody>();


        Destroy(gameObject,5f);
        switch (WeaponManager.instance.weaponType)
        {
            case WeaponManager.WeaponType.HG:
                {
                    damage = 70;
                    speed = 375*20;
                }
                break;
            case WeaponManager.WeaponType.AR:
                {
                    damage = 41;
                    speed = 870*20;
                }
                break;
            case WeaponManager.WeaponType.SR:
                {
                    damage = 130;
                    speed = 954*20;
                }
                break;
            default:
                break;
        }

        bulletRigidbody.velocity = transform.forward * speed *Time.smoothDeltaTime ;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Bullet")
        {
            Bullet bullet = other.GetComponent<Bullet>();

            if (bullet != null)
            {
                Destroy(bullet.gameObject);
            }
            Destroy(gameObject);
        }
        else if (other.CompareTag("BulletSpawner"))
        {
            BulletSpwner bulletSpwner = other.GetComponent<BulletSpwner>();
            if (bulletSpwner != null && bulletSpwner.hp > 0)
            {
                ParticleSystem effectTemp = Instantiate<ParticleSystem>(enemyEffect, transform.position, Quaternion.Euler(-90, 0, 0));
               // Destroy(effectTemp, 1f);
                bulletSpwner.GetDamage(damage);
            }

            Destroy(gameObject);
        }
        else
        {
            ParticleSystem effectTemp = Instantiate<ParticleSystem>(hitEffect, transform.position, Quaternion.Euler(-90, 0, 0));
           // Destroy(effectTemp, 1f);
        }
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

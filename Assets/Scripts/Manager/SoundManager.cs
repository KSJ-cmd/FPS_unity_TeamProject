using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance = null;
    public static SoundManager instance { get { return _instance; } }



    public AudioClip gunFireHG;
    public AudioClip gunFireAR;
    public AudioClip gunFireSR;
    public AudioClip walkingSound;
    public AudioClip runSound;
    public AudioClip reloadSound;
    public AudioClip impactSound;
    public AudioClip explosion;
    public AudioClip itemDrop;
    public AudioClip itemGet;
    public AudioClip bulletMove;
    public AudioClip boltAction;

    AudioSource gunSound;
    AudioSource effectSound;
    AudioSource playerSound;
    AudioSource enemySound;
    AudioSource itemSound;
    AudioSource bulletSound;
    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if(_instance != null)
            {
                Destroy(this.gameObject);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        gunSound = transform.Find("GunSound").GetComponent<AudioSource>();
        effectSound = transform.Find("EffectSound").GetComponent<AudioSource>();
        playerSound = transform.Find("PlayerSound").GetComponent<AudioSource>();
        enemySound = transform.Find("EnemySound").GetComponent<AudioSource>();
        itemSound = transform.Find("ItemSound").GetComponent<AudioSource>();
        bulletSound = transform.Find("BulletSound").GetComponent<AudioSource>();
    }

    //이펙트
    public void ImpactSound()
    {
        effectSound.clip = impactSound;
        effectSound.volume = 0.4f;
        effectSound.Play();
    }
    //플레이어
    public void WalkingSound()
    {
        if (playerSound.isPlaying == false)
        {
            playerSound.clip = walkingSound;
            playerSound.pitch = 1;
            playerSound.volume = 0.2f;
            playerSound.Play();
        }
    }
    public void RunSound()
    {
        if (playerSound.isPlaying == false)
        {
            playerSound.clip = walkingSound;
            playerSound.pitch = 2;
            playerSound.volume = 0.2f;
            playerSound.Play();
        }
    }
    public void WalkingSoundStop()
    {
        playerSound.Stop();
    }
    //총
    public void ReLoadSound()
    {
        gunSound.clip = reloadSound;
        gunSound.Play();
    }
    public void ARSound()
    {
        gunSound.clip = gunFireAR;
        gunSound.Play();
    }
    public void SRSound()
    {
        gunSound.clip = gunFireSR;
        gunSound.Play();
    }
    public void HGSound()
    {
        gunSound.clip = gunFireHG;
        gunSound.Play();
    }
    public void BoltActionSound()
    {
        effectSound.clip = boltAction;
        effectSound.PlayDelayed(0.5f);
    }
    //몬스터
    public void EnemyDieSound()
    {
        enemySound.clip = explosion;
        enemySound.volume = 0.1f;
        enemySound.Play();
    }

    public void BulletMoveSound()
    {
        bulletSound.clip = bulletMove;
        bulletSound.Play();
    }
    //아이템
    public void ItemDropSound()
    {
        itemSound.clip = itemDrop;
        itemSound.Play();
    }
    public void ItemGetSound()
    {
        itemSound.clip = itemGet;
        itemSound.Play();
    }

}

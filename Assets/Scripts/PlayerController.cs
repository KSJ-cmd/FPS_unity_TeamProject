using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    public float speed = 3f;
    public float rotSpeed = 120.0f;


    private Transform tr;

    public int hp = 150;
    public int maxHp = 150;
    public HPBar hpbar;
    public HealthPoint healthPoint;
    // Start is called before the first frame update

    private float spawnRate = 0.1f;
    private float timerAfterSpawn;

    public GameObject playerbulletPrefab;
    public CamRotate cameraArm;
    CharacterController CharacterController;
    public Animator animator;

    //effect
    public ParticleSystem ARFireEffect;
    public ParticleSystem SRFireEffect;


    public float gravity = -9.8f;
    float yVelocity;

    bool playWalk = false;
    bool playRun = false;

    public int keyAmount
    {
        private set;
        get;
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        timerAfterSpawn = 0;
        tr = GetComponent<Transform>();
        CharacterController = GetComponent<CharacterController>();
        WeaponManager.instance.Reload();
        healthPoint.setHealthPoint(hp);
        UIManager.instance.WeaponIconPrint();
        keyAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        WeaponSwap();
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        if (animator != null)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = 10f;
                animator.SetBool("isRun", true);
                //뛰는 사운드
            }
            else
            {
                speed = 5f;
                animator.SetBool("isRun", false);


                //걷는 사운드
            }
        }

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            
            if (playWalk == true && speed == 10)
            {
                SoundManager.instance.WalkingSoundStop();
                playWalk = false;
            }
            else if(playRun == true && speed == 5)
            {
                SoundManager.instance.WalkingSoundStop();
                playRun = false;
            }
            if (animator != null)
            {
                animator.SetBool("isMoving", true);
            }
            if (speed == 10)
            {
                SoundManager.instance.RunSound();
                playRun = true;
            }
            else
            {
                SoundManager.instance.WalkingSound();
                playWalk = true;
            }

        }
        else
        {
            if (animator != null)
            {
                animator.SetBool("isMoving", false);
            }
            SoundManager.instance.WalkingSoundStop();
        }

        Vector3 direction = new Vector3(xInput, 0f, zInput);
        //정규화 벡터의 이동속도를 평균으로 ,대각선과 직선의 이동속도가 같게
        direction.Normalize();

        direction = Camera.main.transform.TransformDirection(direction);
        transform.rotation = Camera.main.transform.rotation;

        //transform.position += direction*Time.deltaTime*speed;

        //중력적용
        yVelocity += gravity * Time.smoothDeltaTime;
        direction.y = yVelocity;
        CharacterController.Move(direction * speed * Time.smoothDeltaTime);

        
        //RaycastHit hit = new RaycastHit();
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //if (Physics.Raycast(ray.origin, ray.direction, out hit))
        //{
        //    cameraArm = FindObjectOfType<CamRotate>();

        //    //Vector3 projectPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
        //    //Vector3 currentPos = transform.position;
        //    //Vector3 rotation = projectPos - currentPos;
        //    //tr.forward = rotation;
        //}


        //Debug.DrawRay(thirdCamPos.position, thirdCamPos.forward * 200.0f, Color.green);

        //RaycastHit temp;

        //if (Physics.Raycast(thirdCamPos.position, thirdCamPos.forward, out temp, 200.0f)) // 카메라의 위치에서 카메라가 바라보는 정면으로 레이를 쏴서 충돌확인
        //{
        //    // 충돌이 검출되면 총알의 리스폰포인트(firePos)가 충돌이 발생한위치를 바라보게 만든다. 
        //    // 이 상태에서 발사입력이 들어오면 총알은 충돌점으로 날아가게 된다.
        //    firePos.LookAt(temp.point);
        //    Debug.DrawRay(firePos.position, firePos.forward * 200.0f, Color.cyan); // 이 레이는 앞서 선언한 디버그용 레이와 충돌점에서 교차한다
        //}


        timerAfterSpawn += Time.deltaTime;

        switch (WeaponManager.instance.weaponType)
        {
            case WeaponManager.WeaponType.HG:
                {
                    spawnRate = 0.4f;
                    if (Input.GetButtonDown("Fire1") && timerAfterSpawn >= spawnRate && WeaponManager.instance.currentMagazine > 0)
                    {
                        animator.SetTrigger("HGShot");
                        //탄환 발사 사운드
                        SoundManager.instance.HGSound();
                        //Vector3 playerPos = Camera.main.WorldToScreenPoint(gameObject.transform.position);
                        //Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,playerPos.z));
                        timerAfterSpawn = 0;
                        GameObject bulletSpawn = gameObject.transform.Find("BulletSpawn").gameObject;

                        GameObject bullet = Instantiate(playerbulletPrefab, bulletSpawn.transform.position, Quaternion.identity);
                        bullet.transform.forward = Camera.main.transform.forward;
                        //bullet.transform.LookAt(mousePosition);

                        //effect
                        GameObject gunFireEffect = gameObject.transform.Find("GunFireEffectPosition").gameObject;
                        Instantiate(ARFireEffect, gunFireEffect.transform.position, Quaternion.identity);


                        WeaponManager.instance.currentMagazine--;
                    }
                }
                break;
            case WeaponManager.WeaponType.AR:
                {
                    spawnRate = 0.1f;
                    if (Input.GetButton("Fire1") && timerAfterSpawn >= spawnRate && WeaponManager.instance.currentMagazine > 0)
                    {
                        animator.SetTrigger("ARShot");
                        GameObject gunFireEffect = gameObject.transform.Find("GunFireEffectPosition").gameObject;
                        Instantiate(ARFireEffect, gunFireEffect.transform.position, Quaternion.identity);
                        //탄환 발사 사운드
                        SoundManager.instance.ARSound();
                        //Vector3 playerPos = Camera.main.WorldToScreenPoint(gameObject.transform.position);
                        //Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,playerPos.z));
                        timerAfterSpawn = 0;
                        GameObject bulletSpawn = gameObject.transform.Find("BulletSpawn").gameObject;

                        GameObject bullet = Instantiate(playerbulletPrefab, bulletSpawn.transform.position, Quaternion.identity);
                        bullet.transform.forward = Camera.main.transform.forward;

                        //effect
        

                        //bullet.transform.LookAt(mousePosition);
                        WeaponManager.instance.currentMagazine--;
                    }
                }
                break;
            case WeaponManager.WeaponType.SR:
                {
                    spawnRate = 2f;
                    if (Input.GetButtonDown("Fire1") && timerAfterSpawn >= spawnRate && WeaponManager.instance.currentMagazine > 0)
                    {
                        GameObject gunFireEffect = gameObject.transform.Find("GunFireEffectPosition").gameObject;
                        Instantiate(SRFireEffect, gunFireEffect.transform.position, Quaternion.identity);
                        animator.SetTrigger("BoltAction");
                        SoundManager.instance.BoltActionSound();
                        //탄환 발사 사운드
                        SoundManager.instance.SRSound();
                        //Vector3 playerPos = Camera.main.WorldToScreenPoint(gameObject.transform.position);
                        //Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,playerPos.z));
                        timerAfterSpawn = 0;
                        GameObject bulletSpawn = gameObject.transform.Find("BulletSpawn").gameObject;

                        GameObject bullet = Instantiate(playerbulletPrefab, bulletSpawn.transform.position, Quaternion.identity);
                        bullet.transform.forward = Camera.main.transform.forward;
                        //bullet.transform.LookAt(mousePosition);

                        //effect
                        

                        WeaponManager.instance.currentMagazine--;
                    }
                }
                break;
            default:
                break;
        }
    }

    void WeaponSwap()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            WeaponManager.instance.SaveMagazine();
            WeaponManager.instance.weaponType = WeaponManager.WeaponType.AR;
            WeaponManager.instance.LoadMagazine();
            UIManager.instance.WeaponIconPrint();
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            WeaponManager.instance.SaveMagazine();
            WeaponManager.instance.weaponType = WeaponManager.WeaponType.SR;
            WeaponManager.instance.LoadMagazine();
            UIManager.instance.WeaponIconPrint();
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            WeaponManager.instance.SaveMagazine();
            WeaponManager.instance.weaponType = WeaponManager.WeaponType.HG;
            WeaponManager.instance.LoadMagazine();
            UIManager.instance.WeaponIconPrint();
        }
    }
    void Reload()
    {
        animator.SetTrigger("Reload");
        //재장전 사운드
        SoundManager.instance.ReLoadSound();
        WeaponManager.instance.Reload();
    }
    void Die()
    {
        //죽는 사운드
        //gameObject.SetActive(false);
        animator.SetTrigger("Die");
        GameManager gameManager = FindObjectOfType<GameManager>();

        gameManager.EndGame();
    }
    public void GetDamage(int damage)
    {
        if (hp < 0)
        {
            return;
        }
        //피격사운드
        SoundManager.instance.ImpactSound();
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
            Die();
        }
        hpbar.SetHP(hp);
        healthPoint.setHealthPoint(hp);
    }
    public void GetHeal(int heal)
    {
        //아이템획득 사운드
        hp += heal;
        if (hp > maxHp)
            hp = maxHp;
        hpbar.SetHP(hp);
        healthPoint.setHealthPoint(hp);
        
    }
    public void GetKey()
    {
        //아이템획득 사운드
        keyAmount++;
        Debug.Log("Get Key");
    }
    public void UseKey()
    {
        //아이템획득 사운드
        keyAmount--;
        Debug.Log("Get Key");
    }
}

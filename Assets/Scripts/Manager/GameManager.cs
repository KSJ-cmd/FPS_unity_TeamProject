using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject gameoverText;

    public Text timeText;
    public Text recordText;
    public TextMeshProUGUI magazine;

    public GameObject level;
    public GameObject bullerSpawnerPrefab;

    int prevTime;
    int prevItemCheck;
    public float levelTime;

    private Vector3[] bulletSpawners = new Vector3[4];


    private float surviveTime;
    private bool isGameover;

    int prevEventTime;


    private static GameManager _instance = null;
    public static GameManager instance { get { return _instance; } }
    public void EndGame()
    {
        isGameover = true;
        gameoverText.SetActive(true);

        float bestTime = PlayerPrefs.GetFloat("BestTime");

        if (surviveTime > bestTime)
        {
            bestTime = surviveTime;
            PlayerPrefs.SetFloat("BestTime", bestTime);
        }

        recordText.text = "BestTime:" + (int)bestTime;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Invoke("Exit", 5f);
    }
    public void Exit()
    {
        Application.Quit();
    }
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
        Application.targetFrameRate = 60;
    }
    // Start is called before the first frame update
    void Start()
    {
        surviveTime = 0;
        isGameover = false;
        prevTime = 0;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameover)
        {
            levelTime += Time.deltaTime;
            surviveTime += Time.deltaTime;
            timeText.text = "Time:" + (int)surviveTime;
            magazine.text = WeaponManager.instance.currentMagazine+ "/" + WeaponManager.instance.haveMagazine;

            int currTime = (int)(surviveTime % 10f);
            //Debug.Log(prevTime + ", " + currTime);

        }
        else
        {
            //if (Input.GetKeyDown(KeyCode.R))
            //{
            //    isGameover = false;
            //    SceneManager.LoadScene("SampleScene2");
            //}
        }
    }
    public void DieBulletSpawner(GameObject spawner)
    {
       
    }
    
}

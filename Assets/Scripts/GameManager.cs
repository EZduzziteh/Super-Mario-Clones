using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    static GameManager _instance = null;
    
public static GameManager instance
    {
        get { return _instance; }
        set { _instance = value; }
    }
    public MusicManager mus;

   
         public int lives;
        public int score;
        public int coins;
    public Text scoretext;
    public Text cointext;
    public Text timetext;
    public Text livestext;
    public Image mariopic;
    public Image tran;
    public float timer;
    public GameObject canvas;
    bool hasplayedtimelow;
   

    // Use this for initialization
    void Start()
    {
        
        hasplayedtimelow = false;
        
        timer = 400;
        lives = 0;
        score = 0;
        coins = 0;
        livestext.text = "x "+lives.ToString();
        if (instance)
            DestroyImmediate(gameObject);
        else
        {
            DontDestroyOnLoad(this);
            instance = this;
        }

    }
    
    
    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("Fire1") && SceneManager.GetActiveScene().name == "Lose")
        {
            GoToTitle();
        }
        if (Input.GetButtonDown("Fire1") && SceneManager.GetActiveScene().name == "Win")
        {
            GoToTitle();
        }
        if (!mus) {
            mus = FindObjectOfType<MusicManager>();
            Debug.Log("fidnign mus manager");
        }

        if (timer < 100 && SceneManager.GetActiveScene().name == "Level 2-1")
        {
            if (!hasplayedtimelow)
            {
                hasplayedtimelow = true;
                //plays the low time music
                mus.PlayLowTime();
            }
        }
        if (SceneManager.GetActiveScene().name == "Win")
        {
            if (timer < 0)
            {
                GoToTitle();
                Destroy(gameObject);
            }
        }else if (SceneManager.GetActiveScene().name == "Lose")
        {
            if (timer < 0)
            {
                GoToTitle();
                Destroy(gameObject);
            }
        }
        if (timer < 400)
        {
            tran.gameObject.SetActive(false);
            mariopic.gameObject.SetActive(false);
            livestext.gameObject.SetActive(false);
        }
        timer -= Time.deltaTime;
        scoretext.text = score.ToString();
        cointext.text = "x "+coins.ToString();
        timetext.text = timer.ToString();
        if (timer <= 0)
        {
            if (SceneManager.GetActiveScene().name == "Level 2-1")
            {
                GoToLose();
            }
        }
	}
    public void GoToTitle()
    {
        canvas.SetActive(false);
        SceneManager.LoadScene("Title");
    }
    public void GoToWin()
    {
        timer = 10;
        
        SceneManager.LoadScene("win");
    }
    public void GoToLose()
    {
        timer = 5;
        
        SceneManager.LoadScene("Lose");
    }
    public void ReloadLevel()
    {
        
        
        timer = 401;
        
        lives--;
        if (lives <= 0)
        {

            GoToLose();

        }
        else
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            canvas.SetActive(true);
            tran.gameObject.SetActive(true); ;
            mariopic.gameObject.SetActive(true);
            livestext.gameObject.SetActive(true);
            livestext.text = "x " + lives.ToString();
        }
    }
    public void StartGame()
    {
        
        score = 0;
        timer = 401;
        coins = 0;
        lives=3;


        tran.gameObject.SetActive(true);
        mariopic.gameObject.SetActive(true);
        livestext.gameObject.SetActive(true);
        SceneManager.LoadScene("Level 2-1");
        canvas.SetActive(true);
        livestext.text = "x " + lives.ToString();
    }
    public void QuitApp()
    {
        Application.Quit();
    }
}



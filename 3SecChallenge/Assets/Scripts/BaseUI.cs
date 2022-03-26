using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BaseUI : MonoBehaviour
{
    public Canvas canvas;
    public Text scoreText; 
    public Text hpText; 
    public int hp=3;
    public int score=0;
    public GameManager gameManager;
    public AudioSource audio;
    public AudioSource damagedS;
   
    int highScore;
    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt("highScore");
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(canvas);
        Time.timeScale = 1f;
     
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Lobby")
        {
            scoreText.text = "HighScore : " + highScore.ToString();
            Time.timeScale = 1f;
            hpText.gameObject.SetActive(false);
            audio.Stop();
        }
        else if (SceneManager.GetActiveScene().name != "GameOver")
        {
            scoreText.text = "Score : " + score.ToString();
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            score++;
        }
        else
        {
            if (score > highScore)
                highScore = score;

            PlayerPrefs.SetInt("highScore", highScore);
        }


        if(gameManager.isFailed)
        {
            damagedS.Play();
            hp--;
            gameManager.isFailed = false;
        }

        hpText.text = "Hp : " + hp.ToString();

        if(hp == 0)
        {
            SceneManager.LoadScene("GameOver");
            hp = -1;
        }

        

        if (score == 2000)
            Time.timeScale = 1.2f;
        if (score == 4000)
            Time.timeScale = 1.5f;
        if (score == 6000)
            Time.timeScale = 1.8f;
        if (score == 8000)
            Time.timeScale = 2f;

        gameManager.score = score;
        FirstS();
    }

    void FirstS()
    {
        if(SceneManager.GetActiveScene().name == "First Stage")
        {
            hp = 3;
            hpText.gameObject.SetActive(true);
            score = 0;
            audio.Play();
        }
    }
}

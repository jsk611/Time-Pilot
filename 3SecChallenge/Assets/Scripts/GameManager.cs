using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Image timeBar;
    bool _isFinished;
    public bool isFailed;

    public bool isFinished
    {
        get { return _isFinished; }
    }

    int _score;
    public int score
    {
        set { _score = value; }
    }

    float t = 0;
    // Start is called before the first frame update
    void Start()
    {
        _isFinished = false;
        Screen.SetResolution(1680, 1050, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Lobby")
            return;
        timeBar.fillAmount -= Time.deltaTime * 0.33f;
        if (timeBar.fillAmount <= 0)
        {
            _isFinished = true;
            t += Time.deltaTime;
            if (t >= 2)
            {
                NextStage();
                t = 0;
            }
        }
        if (timeBar == null)
        {
            _isFinished = false;
            timeBar = GameObject.Find("Canvas").transform.Find("Time Bar").GetComponent<Image>();
        }
    }

    void NextStage()
    {
        if(SceneManager.GetActiveScene().name == "Stage 0" || _score >= 6000)
        {
            int randomStage = Random.Range(1, 6);
            switch(randomStage)
            {
                case 1: SceneManager.LoadScene("Stage 1");break;
                case 2:int a = Random.Range(0, 2);
                    switch(a)
                    {
                        case 0: SceneManager.LoadScene("Stage 2"); break;
                        case 1: SceneManager.LoadScene("Stage 2-1"); break;
                    }break;
                case 3: SceneManager.LoadScene("Stage 3"); break;
                case 4: SceneManager.LoadScene("Stage 4"); break;
                case 5: SceneManager.LoadScene("Stage 5"); break;

            }
            
        }
        else
        {
            SceneManager.LoadScene("Stage 0");

        }

    }
}

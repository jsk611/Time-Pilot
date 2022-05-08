using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Fade 
{
    public GameObject player;
    public GameObject baseUI;
    public int score, hp;
    
    public Text scoreText;
    [SerializeField] GameObject[] hpImgs;

    public float time;
    [SerializeField] Image timeBar;
    public Image fade;

    bool success;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(baseUI);
        time = 3;
        hp = 4;
        NextStage();
    }

    private void Update()
    {
        score += (int)(Time.deltaTime*200);
        scoreText.text = score.ToString() + "��";

        if (Input.GetKeyDown(KeyCode.K))
            DecreaseHp();

        if (time > 0)
            time -= Time.deltaTime;
        else if(time < 0)
        {
            StartCoroutine(TimeOut());
            time = 0;
        }

        timeBar.fillAmount = time / 3f;
    }

    IEnumerator TimeOut()
    {
        yield return new WaitForEndOfFrame();
        player.layer = 6;
        if(success)
        {
            Debug.Log("O");
        }
        else
        {
            Debug.Log("X");
        }
        yield return new WaitForSeconds(1.5f);
        //destroy
        for(int i=3; i>0; i--)
        {
            //countdown
            Debug.Log(i.ToString());

            if (i == 1)
                StartCoroutine(FadeOut(fade, 0.5f));
            yield return new WaitForSeconds(0.5f);
        }
        NextStage();
        player.layer = 3;
    }
    public void Succeed()
    {
        success = true;
    }
    public void Failed(bool timeover)
    {
        success = false;
        if (timeover)
            time = 0.001f;

        DecreaseHp();
    }
    void DecreaseHp()
    {
        //�̼� ���� �� �÷��̾� ü�� ����
        hp--;

        for (int i = hp; i < 4; i++)
        {
            hpImgs[i].SetActive(false);
        }


        if (hp <= 0)
            GameOver();
    }

    public void NextStage()
    {
        //���� �������� �̵�
        int r;
        do
        {
            r = Random.Range(1, 4);
        } while (("Stage " + r.ToString()).Equals(SceneManager.GetActiveScene().name));
        
        SceneManager.LoadScene("Stage "+r.ToString());
        time = 3;

        StartCoroutine(FadeIn(fade, 0.3f));

    }

    public void GameOver()
    {
        //���ӿ����� �̵�

        if (score >= PlayerPrefs.GetInt("maxScore", 0))
            PlayerPrefs.SetInt("maxScore", score);
        Destroy(baseUI);
        Destroy(player);
        Destroy(gameObject);
    }
}

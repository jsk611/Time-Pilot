using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject baseUI;
    public int score, hp;
    
    public Text scoreText;
    [SerializeField] GameObject[] hpImgs;

    public float time;
    [SerializeField] Image timeBar;

    public bool success;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(baseUI);
        time = 3;
        hp = 4;
    }

    private void Update()
    {
        score += (int)(Time.deltaTime*200);
        scoreText.text = score.ToString() + "��";
        NextStage();

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
        if(success)
        {
            Debug.Log("O");
        }
        else
        {
            Debug.Log("X");
            DecreaseHp();
        }
        yield return new WaitForSeconds(1.5f);
        //destroy
        for(int i=3; i>0; i--)
        {
            //countdown
            Debug.Log(i.ToString());
            yield return new WaitForSeconds(1f);
        }
        NextStage();
    }

    public void DecreaseHp()
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

        if (Input.GetKeyDown(KeyCode.N))
        {
            SceneManager.LoadScene("Stage 1");
            time = 3;

        }

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

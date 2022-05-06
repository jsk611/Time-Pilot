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
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(baseUI);

        hp = 4;
    }

    private void Update()
    {
        score += (int)(Time.deltaTime*200);
        scoreText.text = score.ToString() + "점";
        NextStage();

        if (Input.GetKeyDown(KeyCode.K))
            DecreaseHp();
    }

    public void DecreaseHp()
    {
        //미션 실패 시 플레이어 체력 감소
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
        //랜덤 스테이지 이동

        if (Input.GetKeyDown(KeyCode.N))
            SceneManager.LoadScene("Stage 1");
    }

    public void GameOver()
    {
        //게임오버씬 이동

        if (score >= PlayerPrefs.GetInt("maxScore", 0))
            PlayerPrefs.SetInt("maxScore", score);
        Destroy(baseUI);
        Destroy(player);
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
    public GameObject player;
    public GameObject baseUI;
    public GameObject walls;
    public int score, hp;
    
    public Text scoreText;
    [SerializeField] GameObject[] hpImgs;

    public float time;
    [SerializeField] Image timeBar;
    public Image fade;
    [SerializeField] GameObject timeOutUI;
    [SerializeField] GameObject O, X, countdown;
    bool success;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(baseUI);
        DontDestroyOnLoad(walls);
        DontDestroyOnLoad(timeOutUI);
        time = 3;
        hp = 4;
        NextStage();
    }

    private void Update()
    {

        if (time > 0)
            time -= Time.deltaTime;
        else if(time < 0)
        {
            StartCoroutine(TimeOut());
            time = 0;
        }

        timeBar.fillAmount = time / 3f;

        Time.timeScale = score > 15000 ? 1.5f : 1f + 0.5f / (15000f / score); //시간 가속
    }
    private void FixedUpdate()
    {
        score += (int)(Time.deltaTime*150);
        scoreText.text = score.ToString();
        
    }
    IEnumerator TimeOut()
    {
        yield return new WaitForEndOfFrame();
        player.layer = 6;
        if(success)
        {
            O.SetActive(true);
        }
        else
        {
            X.SetActive(true);
        }
        yield return new WaitForSeconds(1.5f);
        O.SetActive(false);
        X.SetActive(false);

        countdown.SetActive(true);
        Text count = countdown.GetComponentInChildren<Text>();
        for(int i=3; i>0; i--)
        {
            //countdown
            count.text = i.ToString();
            
            if (i == 1)
                StartCoroutine(FadeOut(fade, 0.5f));
            yield return new WaitForSeconds(0.5f);
        }
        countdown.SetActive(false);
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
        //게임오버씬 이동

        if (score >= PlayerPrefs.GetInt("maxScore", 0))
            PlayerPrefs.SetInt("maxScore", score);
        Destroy(baseUI);
        Destroy(player);
        Destroy(walls);
        Destroy(timeOutUI);
        Destroy(gameObject);
    }

    public IEnumerator FadeIn(Image image, float time)
    {
        Debug.Log("aa");
        float a = 1;
        do
        {
            a -= Time.deltaTime / time;
            image.color = new Color(image.color.r, image.color.g, image.color.b, a);
            yield return new WaitForEndOfFrame();
        } while (image.color.a > 0);


    }

    public IEnumerator FadeOut(Image image, float time)
    {
        float a = 0;
        do
        {
            a += Time.deltaTime / time;
            image.color = new Color(image.color.r, image.color.g, image.color.b, a);
            yield return new WaitForEndOfFrame();
        } while (image.color.a < 1);
    }
}

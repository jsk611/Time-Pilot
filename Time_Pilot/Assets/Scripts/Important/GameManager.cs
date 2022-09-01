using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour 
{
    public GameObject player;
    public GameObject baseUI;
    public int hp;
    public float score;
    
    public Text scoreText;
    [SerializeField] GameObject[] hpImgs;

    //public float time;
    //[SerializeField] Image timeBar;
    public Image fade;
    [SerializeField] GameObject timeOutUI;
    [SerializeField] GameObject O, X, countdown;
    [SerializeField] GameObject upgradeUI;
    public bool success;

    int checkPoint;
    int level;

    bool isCheckpoint;

    [SerializeField] AudioMixerGroup bgm, effect;
    public SoundManager sound = new SoundManager();
    [SerializeField] AudioClip[] clips;
    [SerializeField] AudioClip bgmClip;


    void Start()
    {
        sound.Init(bgm, effect);
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(baseUI);
        DontDestroyOnLoad(timeOutUI);
        DontDestroyOnLoad(upgradeUI);
        score = 2022;
        checkPoint = 1900;
        level = 0;
        hp = 4;
        NextStage();
        sound.Play(bgmClip, Define.Sound.Bgm, 1, 0.3f);
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.P))
        {
            Time.timeScale = 0;
            return;
        }
        Time.timeScale = score <= 0 ? 1.75f : 1f + 0.75f * ((2022-score) / 2022); //시간 가속
        sound.ChangePitch(Time.timeScale);
        if ((int)score == checkPoint)
            isCheckpoint = true;
    }
    private void LateUpdate()
    {
        score -= Time.deltaTime*4;
        scoreText.text = ((int)score).ToString();
        
    }
    public IEnumerator TimeOut()
    {
        yield return new WaitForEndOfFrame();
        player.layer = 6;
        if(success)
        {
            sound.Play(clips[0], Define.Sound.Effect, 1, 0.5f);
            O.SetActive(true);
        }
        else
        {
            sound.Play(clips[1], Define.Sound.Effect, 1, 0.5f);
            X.SetActive(true);
        }
        yield return new WaitForSeconds(0.75f);
        O.SetActive(false);
        X.SetActive(false);
        if(isCheckpoint)
        {
            Upgrade();
            isCheckpoint = false;
        }
        yield return new WaitForSeconds(0.01f);
        countdown.SetActive(true);
        Text count = countdown.GetComponentInChildren<Text>();
        for(int i=3; i>0; i--)
        {
            //countdown
            count.text = i.ToString();
            sound.Play(clips[2], Define.Sound.Effect);
            if (i == 1)
                StartCoroutine(FadeOut(fade, 0.75f));
            yield return new WaitForSeconds(0.75f);
        }
        countdown.SetActive(false);
        sound.Play(clips[2], Define.Sound.Effect, 2);
        NextStage();
        player.layer = 3;
    }
    public void Succeed()
    {
        success = true;
    }
    public void Failed()
    {
        success = false;
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
            StartCoroutine(GameOver());
    }
    public void IncreaseHp()
    {
        hp++;
        for (int i = 0; i < hp; i++)
        {
            hpImgs[i].SetActive(true);
        }
    }

    void Upgrade()
    {
        upgradeUI.SetActive(true);
        upgradeUI.GetComponentInChildren<UpgradeLogic>().ResetChoice();
        checkPoint -= 100 + 20 * level++;
    }
    public void NextStage()
    {
        //랜덤 스테이지 이동
        int r;
        do
        {
            r = Random.Range(1, 11);
        } while (("Stage " + r.ToString()).Equals(SceneManager.GetActiveScene().name));
        //r = 3;
        SceneManager.LoadScene("Stage "+r.ToString());

        StartCoroutine(FadeIn(fade, 0.3f));
        Succeed();
    }

    public IEnumerator GameOver()
    {
        PlayerPrefs.SetInt("score", (int)score);

        string[] grade = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12",
            "13", "14", "15", "16", "17", "18","19","20","21","22","23","24","25" };
        int i = 0;
        while(i<25)
        {
            if(score >= (19-i)*100)
            {
                PlayerPrefs.SetString("grade", grade[i]);
                break;
            }
            i++;
        }
        if(i >= 25)
            PlayerPrefs.SetString("grade", grade[24]);


        if (score < PlayerPrefs.GetInt("maxScore", 9999))
        {
            PlayerPrefs.SetInt("maxScore", (int)score);
            PlayerPrefs.SetString("maxGrade", PlayerPrefs.GetString("grade", "error"));
            PlayerPrefs.SetInt("isUpdated", 1);
        }
        else PlayerPrefs.SetInt("isUpdated", 0);
        //게임오버씬 이동
        StartCoroutine(FadeOut(fade, 1f));
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("GameOver");

        Destroy(baseUI);
        Destroy(player);
        Destroy(timeOutUI);
        Destroy(upgradeUI);
        Destroy(gameObject);
        Destroy(GameObject.Find("@Sound"));
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

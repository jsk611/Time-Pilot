using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PiggyBank
{
    public int startPoint;
    int point;
    public bool isEarned = false;
    public void Init(int point)
    {
        this.startPoint = point;
        this.point = startPoint;
    }
    public void ReachCheckpoint()
    {
        this.point--;
        if (point == 0)
            isEarned = true;
    }
}
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

    public int checkPoint;
    public int level;

    bool isCheckpoint;

    [SerializeField] AudioMixerGroup bgm, effect;
    public SoundManager sound = new SoundManager();
    [SerializeField] AudioClip[] clips;
    [SerializeField] AudioClip bgmClip;

    public List<PiggyBank> piggyBanks = new List<PiggyBank>();

    public float handicap = 0;

    public bool isInBossStage;
    public int bossStageNum;
    void Start()
    {
        sound.Init(bgm, effect);
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(baseUI);
        DontDestroyOnLoad(timeOutUI);
        DontDestroyOnLoad(upgradeUI);
        score = 2023;
        checkPoint = 1900;
        level = 0;
        hp = 4;
        NextStage();
        sound.Play(bgmClip, Define.Sound.Bgm, 1, 0.6f);
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.P))
        {
            Time.timeScale = 0;
            return;
        }

        Time.timeScale = score <= 0 ? 2f : 1f + 1f * ((2022-score) / 2022); //시간 가속

        sound.ChangePitch(Time.timeScale);

        if ((int)score <= checkPoint)
            isCheckpoint = true;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Lobby");
            Destroy(baseUI);
            Destroy(player);
            Destroy(timeOutUI);
            Destroy(upgradeUI);
            Destroy(gameObject);
            Destroy(GameObject.Find("@Sound"));
        }
    }
    private void LateUpdate()
    {
        if (isInBossStage) return;

        score -= Time.deltaTime * 2;
        if (score < 1900)
            score -= Time.deltaTime * 2;
        if (score < 1600)
            score -= Time.deltaTime * 2;
        if(score < 1000)
            score -= Time.deltaTime * 2;
        if (score < 0)
            score -= Time.deltaTime * 3;
        if (score < -1500)
            score -= Time.deltaTime * 4;
        scoreText.text = ((int)score).ToString();
        
        if(score <= 1939.2f && bossStageNum == 0)
        {
            score = 1939.1f;
            bossStageNum = 1;
            
            isInBossStage = true;
        }
    }
    public IEnumerator TimeOut()
    {
        yield return new WaitForEndOfFrame();
        player.layer = 6;
        if(success)
        {
            sound.Play(clips[0], Define.Sound.Effect, 1);
            O.SetActive(true);
        }
        else
        {
            sound.Play(clips[1], Define.Sound.Effect, 1);
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
        if (SceneManager.GetActiveScene().name.Contains("Boss") && success) StartCoroutine(BonusUpgrade());
        yield return new WaitForSeconds(0.1f);
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

        //보스전 타임오버 시 예외처리
        //if (SceneManager.GetActiveScene().name.Contains("Boss"))
        //{
        //    DecreaseHp();
        //    DecreaseHp();
        //    DecreaseHp();
        //    DecreaseHp();
        //}
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
            StartCoroutine(GameOver());
    }
    public void IncreaseHp()
    {
        if (hp >= 4)
            return;
        hp++;
        for (int i = 0; i < hp; i++)
        {
            hpImgs[i].SetActive(true);
        }
    }

    void Upgrade()
    {
        checkPoint -= 100 + 20 * level++;
        if (hp <= 0)
            return;
        upgradeUI.SetActive(true);
        upgradeUI.GetComponentInChildren<UpgradeLogic>().ResetChoice();

        foreach (PiggyBank p in piggyBanks)
        {
            p.ReachCheckpoint();
            if (p.isEarned)
            {
                if (p.startPoint == 3)
                {
                    IncreaseHp();
                }
                IncreaseHp();
            }
        }
        for(int i=0;i<2;i++)
        {
            foreach(PiggyBank p in piggyBanks)
            {
                if(p.isEarned)
                {
                    piggyBanks.Remove(p);
                    break;
                }
            }
        }

        
    }

    IEnumerator BonusUpgrade()
    {
        for(int i=0; i<3; i++)
        {
            upgradeUI.SetActive(true);
            upgradeUI.GetComponentInChildren<UpgradeLogic>().ResetChoice();
            while (upgradeUI.activeSelf)
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }
    public void NextStage()
    {
        //보스 스테이지 이동
        if (isInBossStage)
        {
            SceneManager.LoadScene("BossStage " + bossStageNum.ToString());
            StartCoroutine(FadeIn(fade, 0.3f));
            return;
        }

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

        string[] grade = new string[] { "Legend", "S+", "S", "A+", "A", "A-", "B+", "B", "B-",
            "C+", "C", "C-","D","E","F","NOOB" };
        int i = 0;
        if (score > 1950) PlayerPrefs.SetString("grade", grade[14]);
        else if (score > 1850) PlayerPrefs.SetString("grade", grade[13]);
        else if (score > 1750) PlayerPrefs.SetString("grade", grade[12]);
        else if (score > 1600) PlayerPrefs.SetString("grade", grade[11]);
        else if (score > 1500) PlayerPrefs.SetString("grade", grade[10]);
        else if (score > 1400) PlayerPrefs.SetString("grade", grade[9]);
        else if (score > 1200) PlayerPrefs.SetString("grade", grade[8]);
        else if (score > 1000) PlayerPrefs.SetString("grade", grade[7]);
        else if (score > 700) PlayerPrefs.SetString("grade", grade[6]);
        else if (score > 400) PlayerPrefs.SetString("grade", grade[5]);
        else if (score > 0) PlayerPrefs.SetString("grade", grade[4]);
        else if (score > -500) PlayerPrefs.SetString("grade", grade[3]);
        else if (score > -1200) PlayerPrefs.SetString("grade", grade[2]);
        else if (score > -3000) PlayerPrefs.SetString("grade", grade[1]);
        else PlayerPrefs.SetString("grade", grade[0]);



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

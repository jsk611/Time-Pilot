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
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(baseUI);
        DontDestroyOnLoad(walls);
        DontDestroyOnLoad(timeOutUI);
        DontDestroyOnLoad(upgradeUI);
        score = 2022;
        checkPoint = 1900;
        level = 0;
        hp = 4;
        NextStage();
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.P))
        {
            Time.timeScale = 0;
            return;
        }
        Time.timeScale = score <= 0 ? 1.75f : 1f + 0.75f * ((2022-score) / 2022); //�ð� ����

        if ((int)score == checkPoint)
            Upgrade();
    }
    private void FixedUpdate()
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
            O.SetActive(true);
        }
        else
        {
            X.SetActive(true);
        }
        yield return new WaitForSeconds(0.75f);
        O.SetActive(false);
        X.SetActive(false);

        countdown.SetActive(true);
        Text count = countdown.GetComponentInChildren<Text>();
        for(int i=3; i>0; i--)
        {
            //countdown
            count.text = i.ToString();
            
            if (i == 1)
                StartCoroutine(FadeOut(fade, 0.75f));
            yield return new WaitForSeconds(0.75f);
        }
        countdown.SetActive(false);
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
        //�̼� ���� �� �÷��̾� ü�� ����
        hp--;

        for (int i = hp; i < 4; i++)
        {
            hpImgs[i].SetActive(false);
        }


        //if (hp <= 0)
        //    GameOver();
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
        //���� �������� �̵�
        int r;
        do
        {
            r = Random.Range(1, 8);
        } while (("Stage " + r.ToString()).Equals(SceneManager.GetActiveScene().name));
        
        SceneManager.LoadScene("Stage "+r.ToString());

        StartCoroutine(FadeIn(fade, 0.3f));
        Succeed();
    }

    public void GameOver()
    {
        //���ӿ����� �̵�

        if (score >= PlayerPrefs.GetInt("maxScore", 0))
            PlayerPrefs.SetInt("maxScore", (int)score);
        Destroy(baseUI);
        Destroy(player);
        Destroy(walls);
        Destroy(timeOutUI);
        Destroy(upgradeUI);
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
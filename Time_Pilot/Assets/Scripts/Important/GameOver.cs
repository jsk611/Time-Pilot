using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject  updateT, btn1, btn2;
    [SerializeField] Text score, grade1,  grade2;
    [SerializeField] Image background;
    [SerializeField] Sprite[] backgroundImgs;
    [SerializeField] AudioClip[] audioClips;
    AudioSource audioSource;

    string msg2 = "µî±Þ :";
    private void Start()
    {
        int i=0;
        int t = PlayerPrefs.GetInt("score");
        if (t > 1900) i = 0;
        else if (t > 1700) i = 1;
        else if (t > 800) i = 2;
        else if (t > -800) i = 3;
        else if (t > -3000) i = 4;
        else i = 5;

        background.sprite = backgroundImgs[0];

        StartCoroutine(GameOverRoutine());

        Time.timeScale = 1f;

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClips[i];
        audioSource.Play();
    }
    public void Restart()
    {
        SceneManager.LoadScene("FirstSetting");
    }
    public void Lobby()
    {
        SceneManager.LoadScene("Lobby");
    }
    IEnumerator GameOverRoutine()
    {
        yield return new WaitForSecondsRealtime(2f);

        score.text = "2022 A.D";
        StartCoroutine(GameOverRoutine2());
        yield return new WaitForSecondsRealtime(1f);


        int s = PlayerPrefs.GetInt("score");
        int t = 2022;
        while(t != s)
        {
            string msg = Mathf.Abs(t).ToString() + (t >= 0 ? " A.D" : " B.C");
            score.text = msg;
            t--;

            if (t == 1900) StartCoroutine(BackgroundChange(1));
            else if (t == 1700) StartCoroutine(BackgroundChange(2));
            else if (t == 800) StartCoroutine(BackgroundChange(3));
            else if (t == -800) StartCoroutine(BackgroundChange(4));
            else if (t == -3000) StartCoroutine(BackgroundChange(5));

            if(t > 1600)
                yield return new WaitForSecondsRealtime(0.02f);
            else if(t > 1000)
                yield return new WaitForSecondsRealtime(0.01f);
            else
                yield return new WaitForSecondsRealtime(0.005f);
        }
        score.text = Mathf.Abs(s).ToString() + (s >= 0 ? " A.D" : " B.C");
        
        if (PlayerPrefs.GetInt("isUpdated") == 1)
            updateT.SetActive(true);

        yield return new WaitForSecondsRealtime(1f);

        foreach (var i in msg2)
        {
            grade1.text += i;
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSecondsRealtime(0.5f);

        foreach (var i in PlayerPrefs.GetString("grade"))
        {
            grade2.text += i;
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSecondsRealtime(0.5f);

        btn1.SetActive(true);
        btn2.SetActive(true);

    }
    IEnumerator GameOverRoutine2()
    {
        float size = 400;
        while(size > 100)
        {
            size -= Time.deltaTime * 300;
            score.fontSize = (int)size;
            yield return new WaitForEndOfFrame();
        }
        score.fontSize = 100;
    }

    IEnumerator BackgroundChange(int imgIdx)
    {
        float a = 0.5f;
        do
        {
            a -= Time.deltaTime / 2f;
            background.color = new Color(background.color.r, background.color.g, background.color.b, a);
            yield return new WaitForEndOfFrame();
        } while (background.color.a > 0);
        background.sprite = backgroundImgs[imgIdx];
        do
        {
            a += Time.deltaTime / 2f;
            background.color = new Color(background.color.r, background.color.g, background.color.b, a);
            yield return new WaitForEndOfFrame();
        } while (background.color.a < 0.5f);
    }
}

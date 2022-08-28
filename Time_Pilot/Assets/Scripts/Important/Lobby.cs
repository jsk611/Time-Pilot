using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour
{
    [SerializeField] Text high, grade;
    [SerializeField] Image fade;
    [SerializeField] GameObject countdown, b1,b2,b3;
    [SerializeField] Image background;
    [SerializeField] List<Sprite> imgs;
    int idx = 7;

    private void Start()
    {
        high.text = "HIGH : " + PlayerPrefs.GetInt("maxScore", 2022).ToString();
        grade.text = "등급 : " + PlayerPrefs.GetString("maxGrade", "데이터 없음");

        int t = PlayerPrefs.GetInt("highScore",2022);
        if (t >= 1900) idx = 8;
        else if (t >= 1700) idx = 9;
        else if (t >= 600) idx = 10;
        else if (t >= -800) idx = 11;
        else if (t >= -3000) idx = 12;
        else idx = 13;
        StartCoroutine(BackgroundChange());
    }
    public void GameStart()
    {
        StartCoroutine(Countdown());
    }
    public void Quit()
    {
        Application.Quit();
    }
    IEnumerator BackgroundChange()
    {
        while(true)
        {
            float a = 0.5f;
            do
            {
                a -= Time.deltaTime / 2f;
                background.color = new Color(background.color.r, background.color.g, background.color.b, a);
                yield return new WaitForEndOfFrame();
            } while (background.color.a > 0);
            background.sprite = imgs[Random.Range(0, idx)];
            do
            {
                a += Time.deltaTime / 2f;
                background.color = new Color(background.color.r, background.color.g, background.color.b, a);
                yield return new WaitForEndOfFrame();
            } while (background.color.a < 0.5f);
            yield return new WaitForSeconds(5f);
        }
    }
    IEnumerator Countdown()
    {
        Destroy(b1);
        Destroy(b2);
        Destroy(b3);
        countdown.SetActive(true);
        Text count = countdown.GetComponentInChildren<Text>();
        for (int i = 3; i > 0; i--)
        {
            //countdown
            count.text = i.ToString();

            if (i == 1)
                StartCoroutine(FadeIn(fade, 0.75f));
            yield return new WaitForSeconds(0.75f);
        }
        countdown.SetActive(false);
        SceneManager.LoadScene("FirstSetting");
    }
    public IEnumerator FadeIn(Image image, float time)
    {
        float a = 0;
        do
        {
            a -= Time.deltaTime / time;
            image.color = new Color(image.color.r, image.color.g, image.color.b, a);
            yield return new WaitForEndOfFrame();
        } while (image.color.a > 0);
    }
}

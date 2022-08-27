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

    private void Start()
    {
        high.text = "HIGH : " + PlayerPrefs.GetInt("maxScore", 2022).ToString();
        grade.text = "등급 : " + PlayerPrefs.GetString("maxGrade", "데이터 없음");
    }
    public void GameStart()
    {
        StartCoroutine(Countdown());
    }
    public void Quit()
    {
        Application.Quit();
    }

    public IEnumerator Countdown()
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
                StartCoroutine(FadeOut(fade, 0.75f));
            yield return new WaitForSeconds(0.75f);
        }
        countdown.SetActive(false);
        SceneManager.LoadScene("FirstSetting");
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

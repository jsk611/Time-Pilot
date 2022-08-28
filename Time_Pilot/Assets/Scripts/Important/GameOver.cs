using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject  updateT, btn1, btn2;
    [SerializeField] Text score1, score2, grade1,  grade2;
    [SerializeField] Image background;
    [SerializeField] Sprite[] backgroundImgs;

    string msg1 = "도착한 연도 :";
    string msg2 = "등급 :";
    private void Start()
    {
        int i=0;
        int t = PlayerPrefs.GetInt("score");
        if (t >= 1900) i = 0;
        else if (t >= 1700) i = 1;
        else if (t >= 600) i = 2;
        else if (t >= -800) i = 3;
        else if (t >= -3000) i = 4;
        else i = 5;

        background.sprite = backgroundImgs[i];

        StartCoroutine(GameOverRoutine());
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
        yield return new WaitForSecondsRealtime(1f);

        foreach(var i in msg1)
        {
            score1.text += i;
            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSecondsRealtime(1f);

        int t = PlayerPrefs.GetInt("score");
        string msg = Mathf.Abs(t).ToString() + (t >= 0 ? " A.D" : " B.C");
        foreach (var i in msg)
        {
            score2.text += i;
            yield return new WaitForSeconds(0.2f);
        }       
        if (PlayerPrefs.GetInt("isUpdated") == 1)
            updateT.SetActive(true);

        yield return new WaitForSecondsRealtime(2f);

        foreach (var i in msg2)
        {
            grade1.text += i;
            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSecondsRealtime(1f);

        foreach (var i in PlayerPrefs.GetString("grade"))
        {
            grade2.text += i;
            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSecondsRealtime(1f);

        btn1.SetActive(true);
        btn2.SetActive(true);

    }
}

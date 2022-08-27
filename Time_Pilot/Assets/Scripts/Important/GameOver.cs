using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject score1, grade1, updateT, btn1, btn2;
    [SerializeField] Text score2, grade2;
    private void Start()
    {
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
        score1.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        score2.gameObject.SetActive(true);
        int t = PlayerPrefs.GetInt("score");
        score2.text = Mathf.Abs(t).ToString() + (t>=0?" A.D":" B.C");
        if (PlayerPrefs.GetInt("isUpdated") == 1)
            updateT.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
        grade1.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        grade2.gameObject.SetActive(true);
        grade2.text = PlayerPrefs.GetString("grade");
        yield return new WaitForSecondsRealtime(1f);
        btn1.SetActive(true);
        btn2.SetActive(true);

    }
}

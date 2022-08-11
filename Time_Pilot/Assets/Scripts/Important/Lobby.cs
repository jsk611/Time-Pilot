using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour
{
    public void GameStart()
    {
        SceneManager.LoadScene("FirstSetting");
    }
    public void Quit()
    {
        Application.Quit();
    }
}

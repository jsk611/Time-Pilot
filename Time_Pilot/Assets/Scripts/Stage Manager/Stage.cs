using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject player;
    Player playerScript;
    protected float time;
    protected float timeLimit;

    [SerializeField] protected Image timeBar;
    public void LoadInfo()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<Player>();
    }

    public void SetTime(float time)
    {
        this.time = time;
        this.timeLimit = time;
    }
    public void SetTime(float time, bool timeAttack)
    {
        this.time = time + gameManager.handicap;
        this.timeLimit = time + gameManager.handicap;
    }
    public void Timer()
    {
        if (time > 0)
            time -= Time.deltaTime;
        else if (time < 0)
        {
            StartCoroutine(gameManager.TimeOut());
            time = 0;
        }

        timeBar.fillAmount = time / timeLimit;
    }

    public void PlayerHit()
    {
        if(playerScript.isHit)
        {
            time = 0.001f;
            gameManager.Failed();
            playerScript.isHit = false;
            gameManager.success = false;
        }
    }

    //public void PlayerHit(bool isBossStage)
    //{
    //    if (playerScript.isHit)
    //    {
    //        gameManager.DecreaseHp();
    //        playerScript.isHit = false;
    //    }
    //}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage4Manager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject electron;
    public Text question;
    public GameManager gameManager;
    public Area area;

    bool[] usedPoints = new bool[6];
    bool aaa = true;

    int electNum;
    int rightAnswer;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        electNum = Random.Range(0, 7);
        rightAnswer = Random.Range(0, 2);
        for(int i=0; i<electNum; i++)
        {
            int randNum = Random.Range(0, 6);
            if(!usedPoints[randNum])
            {
                Instantiate(electron, spawnPoints[randNum].position, spawnPoints[randNum].rotation);
                usedPoints[randNum] = true;
            }
            else
            {
                i--;
            }
        }

        switch(electNum)
        {
            case 0:
                if(rightAnswer == 0)
                {
                    question.text = "H+";
                }
                else
                {
                    question.text = "H";
                }
                break;
            case 1:
                if (rightAnswer == 0)
                {
                    question.text = "H";
                }
                else
                {
                    int i = Random.Range(0, 3);
                    switch(i)
                    {
                        case 0: question.text = "He"; break;
                        case 1: question.text = "Li+"; break;
                        case 2: question.text = "Be2+"; break;
                    }
                    
                }
                break;
            case 2:
                if (rightAnswer == 0)
                {
                    int i = Random.Range(0, 3);
                    switch (i)
                    {
                        case 0: question.text = "He"; break;
                        case 1: question.text = "Li+"; break;
                        case 2: question.text = "Be2+"; break;
                    }
                }
                else
                {
                    question.text = "Li";
                }
                break;
            case 3:
                if (rightAnswer == 0)
                {
                    question.text = "Li";
                }
                else
                {
                    question.text = "Be";
                }
                break;
            case 4:
                if (rightAnswer == 0)
                {
                    question.text = "Be";
                }
                else
                {
                    question.text = "B";
                }
                break;
            case 5:
                if (rightAnswer == 0)
                {
                    question.text = "B";
                }
                else
                {
                    question.text = "C";
                }
                break;
            case 6:
                if (rightAnswer == 0)
                {
                    question.text = "C";
                }
                else
                {
                    question.text = "N";
                }
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.isFinished && aaa)
        {
            if(rightAnswer == 0)
            {
                if(!area.isTriggered)
                {
                    Debug.Log("성공");
                }
                else
                {
                    Debug.Log("실패");
                    gameManager.isFailed = true;
                    aaa = false;
                }
            }
            else
            {
                if (area.isTriggered)
                {
                    Debug.Log("성공");
                }
                else
                {
                    Debug.Log("실패");
                    gameManager.isFailed = true;
                    aaa = false;
                }
            }
        }
    }
}

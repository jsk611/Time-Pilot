using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage2_1Manager : MonoBehaviour
{
    int randNum;

    public Text Question;
    public GameObject function;
    public GameManager gameManager;
    bool isCorrected;
    bool aaa=true;
    // Start is called before the first frame update
    void Start()
    {
        randNum = Random.Range(0, 4);
        switch (randNum)
        {
            case 0: Question.text = "f(x) = sin(x)"; break;
            case 1: Question.text = "f(x) = cos(x)"; break;
            case 2: Question.text = "f(x) = -sin(x)"; break;
            case 3: Question.text = "f(x) = -cos(x)"; break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isFinished)
        {
            if (randNum == 0)
            {
                
                if (function.transform.position.x >= 4.5 && function.transform.position.x <= 5.5)
                {
                    function.transform.position = new Vector3(5, -1, 0);
                    isCorrected = true;
                }
                else if (function.transform.position.x >= -5.5 && function.transform.position.x <= -4.5)
                {
                    function.transform.position = new Vector3(-5, -1, 0);
                    isCorrected = true;
                }
                

            }
            else if(randNum == 1)
            {
                if (function.transform.position.x <= 3 && function.transform.position.x >= 2)
                {
                    function.transform.position = new Vector3(2.5f, -1, 0);
                    isCorrected = true;
                }
                
            }
            else if(randNum == 2)
            {
                if (function.transform.position.x >= -0.5 && function.transform.position.x <= 0.5)
                {
                    function.transform.position = new Vector3(0, -1, 0);
                    isCorrected = true;
                }
            }
            else
            {
                if (function.transform.position.x <= -2 && function.transform.position.x >= -3)
                {
                    function.transform.position = new Vector3(-2.5f, -1, 0);
                    isCorrected = true;
                }
            }

            if (isCorrected)
            {
            
                Debug.Log("성공!");
            
            }

            else if(aaa)
            {
                Debug.Log("실패!");
                gameManager.isFailed = true;
                aaa = false;
            }
        }
        

    }
}

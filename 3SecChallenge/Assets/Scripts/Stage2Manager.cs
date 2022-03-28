using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage2Manager : MonoBehaviour
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
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        randNum = Random.Range(0, 2);
        switch(randNum)
        {
            case 0:Question.text = "f(x) = x² - 4";break;
            case 1:Question.text = "f(x) = -x² + 4";break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(randNum == 0)
        {
            if (function.transform.eulerAngles.z >= 160 && function.transform.eulerAngles.z <= 200)
            {
                function.transform.eulerAngles = new Vector3(0, 0, 180);
                isCorrected = true;
            }
          
        }
        else
        {
            if (function.transform.eulerAngles.z <= 20 && function.transform.eulerAngles.z >= -20)
            {
                function.transform.eulerAngles = new Vector3(0, 0, 0);
                isCorrected = true;
            }
        }

        if(isCorrected)
        {
            function.GetComponent<Rigidbody2D>().freezeRotation = true;
            if(gameManager.isFinished)
            {
                Debug.Log("성공!");
            }
        }

        if (gameManager.isFinished && !isCorrected && aaa)
        {
            Debug.Log("실패!");
            gameManager.isFailed = true;
            aaa = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage1Logic : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] GameObject[] QAreas;

    int[] numArr = new int[7];
    int randNum;
    int answerNum;
    public Text question;
    bool aaa = true;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        //문제 로직 구현
        numArr[0] = Random.Range(10, 51);
        numArr[1] = Random.Range(10, 51);

        randNum = Random.Range(1, 3);
        switch (randNum)
        {
            case 1:
                numArr[2] = numArr[0] + numArr[1];
                question.text = numArr[0].ToString() + " + " + numArr[1].ToString() + " = ?";
                break;
            case 2:
                numArr[2] = numArr[0] - numArr[1];
                question.text = numArr[0].ToString() + " - " + numArr[1].ToString() + " = ?";
                break;
        }

        for (int i = 3; i < 7; i++)
        {
            numArr[i] = numArr[2] + Random.Range(-10, 11);
            if (numArr[i] == numArr[2] || numArr[i] == numArr[i - 1])
                i--;
        }

        answerNum = Random.Range(0, 4);
        QAreas[answerNum].gameObject.tag = "Answer";


        for (int i = 0; i < 4; i++)
        {
            if (QAreas[i].gameObject.tag == "Answer")
            {
                QAreas[i].GetComponentInChildren<TextMesh>().text = numArr[2].ToString();
                continue;
            }

            QAreas[i].GetComponentInChildren<TextMesh>().text = numArr[i + 3].ToString();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.time <= 0)
        {
            for (int i = 0; i < 4; i++)
            {
                if (i == answerNum)
                    continue;
                QAreas[i].SetActive(false);
            }
            if (QAreas[answerNum].GetComponent<QArea>().isTriggered)
            {
                Debug.Log("정답");
                gameManager.success = true;
            }
            else if (aaa)
            {
                Debug.Log("오답");
                gameManager.success = false;
                aaa = false;
            }

        }
    }
}

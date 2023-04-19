using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage7Logic : Stage
{
    [SerializeField] Machumbup[] answers;
    [SerializeField] Text qText;
    int r;
    string[,] questions = 
    {
        {"이 문제의 정답을 ___","맞히다","맞추다"},
        {"공격을 과녁에 ___","맞추다","맞히다"},
        {"이게 __이냐","웬일","왠일"},
        {"__ 느낌이 좋다","왠지","웬지"},
        {"가시 __ 그의 말","돋친","돋힌"},
        {"난 건물주가 __게\n 꿈이다.","되는","돼는"},
        {"이건 먹어도 _","돼","되"},
        {"이 게임은 타임머신으__ 완벽하다.","로써","로서"},
        {"반장으__ 할 \n이야기가 있다.","로서","로써"},
        {"__ 뒤에 만나기로 하자.","며칠","몇일"},
        {"쟁반에 찻잔을 ___.","받치다","받히다"},
        {"바지 길이를 ___.","늘이다","늘리다"},
        {"마당을 ___.","늘리다","늘이다"},
        {"___색 색연필","빨간","빨강"},
        {"_____ 또 하나가 있다.","이 밖에","이밖에"},
        {"어르신의 말씀을 들을 ____은 마음이 뿌듯했습니다.\n(21학년도 6평 15번)","때만큼","때 만큼"},
        {"____ 없다.   \n(21학년도 6평 15번)","너밖에","너 밖에"},
        {"그저 작은 일을 ________ ... \n(21학년도 6평 15번)","도울 뿐이었는데","도울뿐이었는데"},
        {"그 시간에 _________ 게임을 하고 싶었다.   \n(21학년도 6평 15번)","봉사보다는","봉사 보다는"},

    }; //{문제, 정답, 오답}

    // Start is called before the first frame update
    void Start()
    {
        LoadInfo();
        SetTime(5f, true);

        r = Random.Range(0, 2);
        int randAnswer = 0;
        if(Time.timeScale < 1.75f) randAnswer = Random.Range(0, 14);
        else randAnswer = Random.Range(0, questions.Length - 1);

        qText.text = questions[randAnswer, 0];
        
        answers[r].isAnswer = true;
        answers[r].answerText.text = questions[randAnswer,1];
        answers[1-r].isAnswer = false;
        answers[1-r].answerText.text = questions[randAnswer,2];
   
    }

    // Update is called once per frame
    void Update()
    {
        if (time < 0)
        {
            if (answers[r] != null && answers[1 - r] == null)
                gameManager.Succeed();
            else if(player.layer == 3)
                gameManager.Failed();

        }

        Timer();
        PlayerHit();
    }
}

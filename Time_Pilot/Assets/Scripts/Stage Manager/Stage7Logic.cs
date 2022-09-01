using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage7Logic : Stage
{
    [SerializeField] Machumbup[] answers;
    [SerializeField] Text qText;
    int r;
    string[,] questions = new string[17, 3] 
    {
        {"이 문제의 정답을 ___","맞히다","맞추다"},
        {"공격을 과녁에 ___","맞추다","맞히다"},
        {"이게 __이냐","웬일","왠일"},
        {"__ 느낌이 좋다","왠지","웬지"},
        {"가시 __ 그의 말","돋친","돋힌"},
        {"난 건물주가 __게\n 꿈이다.","되는","돼는"},
        {"이건 먹어도 _","돼","되"},
        {"뒤의 차가 앞 차에 ____.","부딪었다","부딪쳤다"},
        {"두 차가 마주 ____.","부딪쳤다","부딪었다"},
        {"이 게임은 타임머신으__ 완벽하다.","로써","로서"},
        {"반장으__ 할 \n이야기가 있다.","로서","로써"},
        {"__ 뒤에 만나기로 하자.","며칠","몇일"},
        {"공부하느라고 밤을 ___","새우다","새다"},
        {"쟁반에 찻잔을 ___.","받치다","받히다"},
        {"____ 쓴게 이 모양이다.","쓰노라고","쓰느라고"},
        {"바지 길이를 ___.","늘이다","늘리다"},
        {"마당을 ___.","늘리다","늘이다"},
    }; //{문제, 정답, 오답}

    // Start is called before the first frame update
    void Start()
    {
        LoadInfo();
        SetTime(4f, true);

        r = Random.Range(0, 2);
        int randAnswer = Random.Range(0, 17);

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

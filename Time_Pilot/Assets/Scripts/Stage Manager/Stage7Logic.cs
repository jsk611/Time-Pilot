using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage7Logic : Stage
{
    [SerializeField] Machumbup[] answers;
    [SerializeField] Text qText;
    int r;
    string[,] questions = new string[12, 3] 
    {
        {"이 문제의 정답을 ___","맞히다","맞추다"},
        {"공격을 과녁에 ___","맞추다","맞히다"},
        {"이게 __이냐","웬일","왠일"},
        {"__ 느낌이 좋다","왠지","웬지"},
        {"가시 __ 그의 말","돋친","돋힌"},
        {"난 건물주가 __게 꿈이다.","되는","돼는"},
        {"이건 먹어도 _","돼","되"},
        {"밥이 다 __","됐다","됬다"},
        {"이 게임은 타임머신으__ 완벽하다.","로써","로서"},
        {"반장으__ 할 이야기가 있다.","로서","로써"},
        {"__ 뒤에 만나기로 하자.","며칠","몇일"},
        {"공부하느라 밤을 __","새우다","새다"}
    }; //{문제, 정답, 오답}

    // Start is called before the first frame update
    void Start()
    {
        LoadInfo();
        SetTime(5f);

        r = Random.Range(0, 2);
        int randAnswer = Random.Range(0, 12);

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

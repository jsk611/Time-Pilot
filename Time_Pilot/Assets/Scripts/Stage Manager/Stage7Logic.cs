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
        {"�� ������ ������ ___","������","���ߴ�"},
        {"������ ���ῡ ___","���ߴ�","������"},
        {"�̰� __�̳�","����","����"},
        {"__ ������ ����","����","����"},
        {"���� __ ���� ��","��ģ","����"},
        {"�� �ǹ��ְ� __�� ���̴�.","�Ǵ�","�Ŵ�"},
        {"�̰� �Ծ _","��","��"},
        {"���� �� __","�ƴ�","���"},
        {"�� ������ Ÿ�Ӹӽ���__ �Ϻ��ϴ�.","�ν�","�μ�"},
        {"������__ �� �̾߱Ⱑ �ִ�.","�μ�","�ν�"},
        {"__ �ڿ� ������� ����.","��ĥ","����"},
        {"�����ϴ��� ���� __","�����","����"}
    }; //{����, ����, ����}

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
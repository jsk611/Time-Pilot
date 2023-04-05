using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialLogic : MonoBehaviour
{
    [SerializeField] Text title;
    [SerializeField] Text introduceText;

    string[] contents1 = 
    {
        "Time Pilot Ʃ�丮�� ���� ���� ȯ���մϴ�!!!",
        "����� Time Corporation�� ������ν� ���ŷ� �ִ��� �ָ� �����ؾ� �մϴ�!",
        "�ð����� ����� ���� ����ؼ� �������� �����ϱ� ������ ����� ���迡 �¼��� �մϴ�!",
        "�̸� ����ϱ� ���� ��� �ʼ� ��ų�� �˷��帮���� �ϰڽ��ϴ�!",
        "���� �� Ž�缱�� �����ϴ� ������� �˾ƾ߰���?",
        "Ž�缱 �̵����� ������ô�!",
        "[ WSAD�� ���� Ž�缱�� ������������! ]"
    };

    int mode;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Introduce());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Introduce()
    {
        foreach (var item in contents1)
        {
            introduceText.text = "";
            foreach (var t in item)
            {
                introduceText.text += t;
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(2f);
        }
    }
}

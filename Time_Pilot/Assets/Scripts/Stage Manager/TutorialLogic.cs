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
        "Time Pilot 튜토리얼에 오신 것을 환영합니다!!!",
        "당신은 Time Corporation의 조종사로써 과거로 최대한 멀리 여행해야 합니다!",
        "시간여행 기술이 아직 빈약해서 목적지에 도달하기 전까지 상당한 위험에 맞서야 합니다!",
        "이를 대비하기 위해 몇가지 필수 스킬을 알려드리도록 하겠습니다!",
        "먼저 이 탐사선을 조종하는 방법부터 알아야겠죠?",
        "탐사선 이동부터 배워봅시다!",
        "[ WSAD를 눌러 탐사선을 움직여보세요! ]"
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

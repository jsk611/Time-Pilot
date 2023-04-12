using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.XR;
using UnityEditor.U2D.Path;

public class TutorialLogic : MonoBehaviour
{
    [SerializeField] Text title;
    [SerializeField] Text introduceText;
    GameObject player;
    Player playerScript;

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
    string[] contents2 =
    {
        "정말 잘하셨어요!",
        "다음은 공격에 대해 알아봅시다!",
        "다양한 위기 상황에서 광자포는 여러 방면에서 쓰입니다.",
        "풍선을 한번 다 터트려 보세요!",
        "[ 마우스 좌클릭으로 광자포를 발사해보세요! ]"
    };
    string[] contents3 =
    {
        "오 조종사의 소질이 있네요!",
        "마지막으로 공격을 피하는 연습을 해봅시다!",
        "대부분의 위기 상황에서 당신은 특정 시간을 버텨야 다음 구간으로 이동할 수 있습니다!",
        "그러기 위해선 위험요소를 잘 피하는 것도 중요하겠죠?",
        "당신의 탐사선의 체력은 화면 가장자리의 톱니바퀴 갯수와 같습니다.",
        "만약 탐사선이 피격당하면, 체력이 하나 닳고 다음 구역으로 비상탈출을 하게 됩니다.(여긴 튜토리얼이니 체력은 닳지 않습니다!)",
        "예행연습으로 제한시간 내에 한번 살아남아 보세요!",
        "[ 제한시간 동안 생존하십시오! ]"
    };
    string[] finish =
    {
        "완벽합니다!",
        "이제 더이상 제가 가르칠 것은 없는거 같네요!",
        "그럼 행운을 빕니다!"
    };

    string[] hitMoment =
    {
        "아 맞으셨군요..",
        "다시 한번 해봅시다!",
        "[ 제한시간 동안 생존하십시오! ]"
    };

    int mode = 0;
    bool modeChangeTrigger = false;

    [Header("Mode 1")]
    public int mode1Num;
    [SerializeField] GameObject tutorialTarget;

    [Header("Mode 2")]
    [SerializeField] GameObject tutorialTarget2;
    [SerializeField] GameObject tutorialTarget3;

    [Header("Mode 3")]
    public int mode3Num;
    [SerializeField] GameObject cannon;
    [SerializeField] GameObject laserCannon;
    [SerializeField] Image timeBar;
    float time = 20f;
    float timeLimit = 20f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<Player>();
        StartCoroutine(Introduce(contents1));
    }

    // Update is called once per frame
    void Update()
    {
        if (modeChangeTrigger)
        {
            StartCoroutine("Mode" + mode.ToString());
            modeChangeTrigger = false;
        }

        if(mode == 3)
        {
            if (time > 0)
                time -= Time.deltaTime;
            else if (time < 0)
            {
                time = 0;
                mode++;
                ResetMode3();
                StartCoroutine(Finish());
            }

            timeBar.fillAmount = time / timeLimit;

            if (playerScript.isHit)
            {
                StopAllCoroutines();
                ResetMode3();
                time = 20f;
                StartCoroutine(HitMoment());
                playerScript.isHit = false;
            }
        }
    }

    IEnumerator Introduce(string[] contents)
    {
        foreach (var item in contents)
        {
            introduceText.text = "";
            foreach (var t in item)
            {
                introduceText.text += t;
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForSeconds(1f);
        }
        mode++;
        modeChangeTrigger = true;
    }
    IEnumerator HitMoment()
    {
        foreach (var item in hitMoment)
        {
            introduceText.text = "";
            foreach (var t in item)
            {
                introduceText.text += t;
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForSeconds(1f);
        }
        modeChangeTrigger = true;
    }
    IEnumerator Finish()
    {
        foreach (var item in finish)
        {
            introduceText.text = "";
            foreach (var t in item)
            {
                introduceText.text += t;
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForSeconds(1f);
        }
        SceneManager.LoadScene("Lobby");
    }

    IEnumerator Mode1()
    {
        title.text = "목표 위치로 이동하세요!";
        Instantiate(tutorialTarget, new Vector3(6,0,0), Quaternion.identity);
        while (mode1Num == 0)
        {
            yield return new WaitForEndOfFrame();
        }
        Instantiate(tutorialTarget, new Vector3(-6,0,0), Quaternion.identity);
        while (mode1Num == 1)
        {
            yield return new WaitForEndOfFrame();
        }
        Instantiate(tutorialTarget, new Vector3(0, 2.5f, 0), Quaternion.identity); 
        while (mode1Num == 2)
        {
            yield return new WaitForEndOfFrame();
        }
        Instantiate(tutorialTarget, new Vector3(0, -2.5f, 0), Quaternion.identity);
        while (mode1Num == 3)
        {
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(Introduce(contents2));
    }
    IEnumerator Mode2 ()
    {
        title.text = "목표물을 맞춰보세요!";
        Instantiate(tutorialTarget2, new Vector3(6, 0, 0), Quaternion.identity);
        Instantiate(tutorialTarget2, new Vector3(-6, 0, 0), Quaternion.identity);
        Instantiate(tutorialTarget2, new Vector3(0, 2.5f, 0), Quaternion.identity);
        Instantiate(tutorialTarget2, new Vector3(0, -2.5f, 0), Quaternion.identity);
        while(FindAnyObjectByType<Target>() != null)
        {
            yield return new WaitForEndOfFrame();
        }
        Instantiate(tutorialTarget2, new Vector3(6, 2.5f, 0), Quaternion.identity);
        Instantiate(tutorialTarget2, new Vector3(-6, 2.5f, 0), Quaternion.identity);
        Instantiate(tutorialTarget2, new Vector3(6, -2.5f, 0), Quaternion.identity);
        Instantiate(tutorialTarget2, new Vector3(-6, -2.5f, 0), Quaternion.identity);
        while (FindAnyObjectByType<Target>() != null)
        {
            yield return new WaitForEndOfFrame();
        }
        title.text = "움직이는 목표물도 맞춰보세요!";
        Instantiate(tutorialTarget3, new Vector3(6, 0, 0), Quaternion.identity);
        Instantiate(tutorialTarget3, new Vector3(-6, 0, 0), Quaternion.identity);
        Instantiate(tutorialTarget3, new Vector3(0, 2.5f, 0), Quaternion.identity);
        Instantiate(tutorialTarget3, new Vector3(0, -2.5f, 0), Quaternion.identity);
        while (FindAnyObjectByType<movingTarget>() != null)
        {
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(Introduce(contents3));
    }

    IEnumerator Mode3 ()
    {
        title.text = "제한 시간 내에 살아남으세요!";
        for (int i = 0; i < 5; i++)
        {
            Instantiate(laserCannon, new Vector3(Random.Range(-5f, 5f), Random.Range(-2f, 2f), 0), Quaternion.identity)
                .GetComponent<LaserShooter>().player = player.transform;
            yield return new WaitForSeconds(1.5f);
        }
        Instantiate(cannon, new Vector3(-6, 1.5f, 0), Quaternion.identity)
                .GetComponent<Cannon>().player = player.transform;
        yield return new WaitForSeconds(3f);

        Instantiate(cannon, new Vector3(6, 1.5f, 0), Quaternion.identity)
                .GetComponent<Cannon>().player = player.transform;
        yield return new WaitForSeconds(3f);

        Instantiate(cannon, new Vector3(0, -1.5f, 0), Quaternion.identity)
                .GetComponent<Cannon>().player = player.transform;
        yield return new WaitForSeconds(3f);
    }

    void ResetMode3()
    {
        foreach(var c in FindObjectsOfType<Cannon>())
        {
            Destroy(c.gameObject);
        }
        foreach (var c in FindObjectsOfType<LaserShooter>())
        {
            Destroy(c.gameObject);
        }
        foreach (var c in FindObjectsOfType<CannonBullet>())
        {
            Destroy(c.gameObject);
        }
    }
}

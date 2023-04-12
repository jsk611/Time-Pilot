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
        "Time Pilot Ʃ�丮�� ���� ���� ȯ���մϴ�!!!",
        "����� Time Corporation�� ������ν� ���ŷ� �ִ��� �ָ� �����ؾ� �մϴ�!",
        "�ð����� ����� ���� ����ؼ� �������� �����ϱ� ������ ����� ���迡 �¼��� �մϴ�!",
        "�̸� ����ϱ� ���� ��� �ʼ� ��ų�� �˷��帮���� �ϰڽ��ϴ�!",
        "���� �� Ž�缱�� �����ϴ� ������� �˾ƾ߰���?",
        "Ž�缱 �̵����� ������ô�!",
        "[ WSAD�� ���� Ž�缱�� ������������! ]"
    };
    string[] contents2 =
    {
        "���� ���ϼ̾��!",
        "������ ���ݿ� ���� �˾ƺ��ô�!",
        "�پ��� ���� ��Ȳ���� �������� ���� ��鿡�� ���Դϴ�.",
        "ǳ���� �ѹ� �� ��Ʈ�� ������!",
        "[ ���콺 ��Ŭ������ �������� �߻��غ�����! ]"
    };
    string[] contents3 =
    {
        "�� �������� ������ �ֳ׿�!",
        "���������� ������ ���ϴ� ������ �غ��ô�!",
        "��κ��� ���� ��Ȳ���� ����� Ư�� �ð��� ���߾� ���� �������� �̵��� �� �ֽ��ϴ�!",
        "�׷��� ���ؼ� �����Ҹ� �� ���ϴ� �͵� �߿��ϰ���?",
        "����� Ž�缱�� ü���� ȭ�� �����ڸ��� ��Ϲ��� ������ �����ϴ�.",
        "���� Ž�缱�� �ǰݴ��ϸ�, ü���� �ϳ� ��� ���� �������� ���Ż���� �ϰ� �˴ϴ�.(���� Ʃ�丮���̴� ü���� ���� �ʽ��ϴ�!)",
        "���࿬������ ���ѽð� ���� �ѹ� ��Ƴ��� ������!",
        "[ ���ѽð� ���� �����Ͻʽÿ�! ]"
    };
    string[] finish =
    {
        "�Ϻ��մϴ�!",
        "���� ���̻� ���� ����ĥ ���� ���°� ���׿�!",
        "�׷� ����� ���ϴ�!"
    };

    string[] hitMoment =
    {
        "�� �����̱���..",
        "�ٽ� �ѹ� �غ��ô�!",
        "[ ���ѽð� ���� �����Ͻʽÿ�! ]"
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
        title.text = "��ǥ ��ġ�� �̵��ϼ���!";
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
        title.text = "��ǥ���� ���纸����!";
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
        title.text = "�����̴� ��ǥ���� ���纸����!";
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
        title.text = "���� �ð� ���� ��Ƴ�������!";
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

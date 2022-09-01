using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UpgradeLogic : MonoBehaviour
{
    [SerializeField] Button[] upgradeBtns;
    [SerializeField] Player player;
    [SerializeField] GameManager gameManager;
    [SerializeField] Sprite[] imgs;
    [SerializeField] GameObject canvas;
    string[,] upgradeTexts = new string[,]
    {
        { "��� ���", "Ÿ�Ӹӽ� ü�� +1" },
        { "����� �ʽð�", "�̵��ӵ� 5 ���" },
        { "�����¿�", "���ݼӵ� 10 ���" },
        { "�Ĺ� �߻��", "�Ѿ��� �ڷε� �߻�˴ϴ�\n���� �ӵ��� 15 ����" },
        { "�¿� �߻��", "�Ѿ��� �¿�ε� �߻�˴ϴ�\n���� �ӵ��� 15 ����" },
        { "���� �߻��", "���� �Ѿ��� �߻�˴ϴ�\n���� �ӵ��� 30 ����" },
    };
    // Start is called before the first frame update
    void Start()
    {
        //ResetChoice();
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = 0;
    }
    public void ResetChoice()
    {
        int l = upgradeTexts.GetLength(0);

        for (int i = 0; i < 3; i++)
        {
            int rand = Random.Range(0, l);
            Debug.Log(rand);
            Image img = upgradeBtns[i].transform.GetChild(0).GetComponent<Image>();
            img.sprite = imgs[rand];
            Text title = upgradeBtns[i].transform.GetChild(1).GetComponent<Text>();
            title.text = upgradeTexts[rand, 0];
            Text info = upgradeBtns[i].transform.GetChild(2).GetComponent<Text>();
            info.text = upgradeTexts[rand, 1];

            upgradeBtns[i].onClick.RemoveAllListeners();
            switch (rand)
            {
                case 0: upgradeBtns[i].onClick.AddListener(GoldenGear); break;
                case 1: upgradeBtns[i].onClick.AddListener(StopWatch); break;
                case 2: upgradeBtns[i].onClick.AddListener(ElectronicGear); break;
                case 3: upgradeBtns[i].onClick.AddListener(BackShooter); break;
                case 4: upgradeBtns[i].onClick.AddListener(LRShooter); break;
                case 5: upgradeBtns[i].onClick.AddListener(SubShooter); break;
            }
        }
    }
    void Chosen()
    {
        canvas.SetActive(false);
    }
    void GoldenGear()
    {
        if (gameManager.hp < 4)
            gameManager.IncreaseHp();
        Chosen();
    }
    void StopWatch()
    {
        player.speed += 0.05f;
        Chosen();
    }
    void ElectronicGear()
    {
        if(player.attackSpeed > 0.1f)
            player.attackSpeed -= 0.1f;
        
        Debug.Log("���ݼӵ� " + player.attackSpeed);
        Chosen();
    }
    void BackShooter()
    {
        player.shooters[0] = true;
        player.shooterImgs[0].SetActive(true);
        player.attackSpeed += 0.15f;
        Debug.Log("���ݼӵ� " + player.attackSpeed);
        Chosen();
    }
    void LRShooter()
    {
        player.shooters[1] = true;
        player.shooterImgs[1].SetActive(true);
        player.shooterImgs[2].SetActive(true);
        player.attackSpeed += 0.15f;
        Debug.Log("���ݼӵ� " + player.attackSpeed);
        Chosen();
    }
    void SubShooter()
    {
        player.shooters[2] = true;
        player.shooterImgs[3].SetActive(true);
        player.attackSpeed += 0.3f;
        Debug.Log("���ݼӵ� " + player.attackSpeed);
        Chosen();
    }
}

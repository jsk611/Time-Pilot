using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeLogic : MonoBehaviour
{
    [SerializeField] Button[] upgradeBtns;
    [SerializeField] Player player;
    [SerializeField] GameManager gameManager;
    [SerializeField] List<Sprite> imgs;
    [SerializeField] GameObject canvas;
    List<string[]> upgradeTexts = new List<string[]>();

    // Start is called before the first frame update
    void Awake()
    {
        upgradeTexts.Add(new string[] { "골든 기어", "타임머신 체력 +1", "0" });
        upgradeTexts.Add(new string[] { "평범한 초시계", "이동속도 10 상승", "1" });
        upgradeTexts.Add(new string[] { "전기태엽", "공격속도 10 상승", "2" });
        upgradeTexts.Add(new string[] { "후방 발사기", "총알이 뒤로도 발사됩니다\n공격 속도가 15 감소", "3" });
        upgradeTexts.Add(new string[] { "좌우 발사기", "총알이 좌우로도 발사됩니다\n공격 속도가 15 감소", "4" });
        upgradeTexts.Add(new string[] { "보조 발사기", "보조 총알이 발사됩니다\n공격 속도가 30 감소", "5" });
        upgradeTexts.Add(new string[] { "머신건", "공격 속도가 30 늘어나는 대신 \n이동 속도가 25% 감소합니다.", "6" });
        upgradeTexts.Add(new string[] { "기어 저금통", "다음 1번째 체크포인트에서 \n타임머신 체력 +1", "7" });
        upgradeTexts.Add(new string[] { "기어 저금통 +", "다음 3번째 체크포인트에서 \n타임머신 체력 +2", "8" });
        upgradeTexts.Add(new string[] { "슬로우 모드", "시간제한 스테이지에서\n제한시간 +0.5초", "9" });
        //ResetChoice();
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = 0;
    }
    public void ResetChoice()
    {
        int l = upgradeTexts.Count;
        //Debug.Log(upgradeTexts[0][0]);
        for (int i = 0; i < 3; i++)
        {
            int rand = Random.Range(0, l);
            Debug.Log(rand);
            Image img = upgradeBtns[i].transform.GetChild(0).GetComponent<Image>();
            img.sprite = imgs[rand];
            Text title = upgradeBtns[i].transform.GetChild(1).GetComponent<Text>();
            title.text = upgradeTexts[rand][0];
            Text info = upgradeBtns[i].transform.GetChild(2).GetComponent<Text>();
            info.text = upgradeTexts[rand][1];

            upgradeBtns[i].onClick.RemoveAllListeners();
            switch (int.Parse(upgradeTexts[rand][2]))
            {
                case 0: upgradeBtns[i].onClick.AddListener(GoldenGear); break;
                case 1: upgradeBtns[i].onClick.AddListener(StopWatch); break;
                case 2: upgradeBtns[i].onClick.AddListener(ElectronicGear); break;
                case 3: upgradeBtns[i].onClick.AddListener(BackShooter); break;
                case 4: upgradeBtns[i].onClick.AddListener(LRShooter); break;
                case 5: upgradeBtns[i].onClick.AddListener(SubShooter); break;
                case 6: upgradeBtns[i].onClick.AddListener(MachineGun); break;
                case 7: upgradeBtns[i].onClick.AddListener(PiggyBank); break;
                case 8: upgradeBtns[i].onClick.AddListener(PiggyBankPlus); break;
                case 9: upgradeBtns[i].onClick.AddListener(SlowMode); break;
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
        player.speed += 0.5f;
        Chosen();
    }
    void ElectronicGear()
    {
        if(player.attackSpeed > 0.1f)
            player.attackSpeed -= 0.1f;
        
        Debug.Log("공격속도 " + player.attackSpeed);
        Chosen();
    }
    void BackShooter()
    {
        player.shooters[0] = true;
        player.shooterImgs[0].SetActive(true);
        player.attackSpeed += 0.15f;
        Debug.Log("공격속도 " + player.attackSpeed);
        upgradeTexts.Remove(upgradeTexts[3]);
        imgs.Remove(imgs[3]);
        Chosen();
    }
    void LRShooter()
    {
        player.shooters[1] = true;
        player.shooterImgs[1].SetActive(true);
        player.shooterImgs[2].SetActive(true);
        player.attackSpeed += 0.15f;
        Debug.Log("공격속도 " + player.attackSpeed);
        upgradeTexts.Remove(upgradeTexts[4]);
        imgs.Remove(imgs[4]);
        Chosen();
    }
    void SubShooter()
    {
        player.shooters[2] = true;
        player.shooterImgs[3].SetActive(true); 
        player.attackSpeed += 0.3f;
        Debug.Log("공격속도 " + player.attackSpeed);
        upgradeTexts.Remove(upgradeTexts[5]);
        imgs.Remove(imgs[5]);
        Chosen();
    }
    void MachineGun()
    {
        player.speed *= 0.75f;
        player.attackSpeed -= 0.3f;
        Chosen();
    }
    void PiggyBank()
    {
        PiggyBank p = new PiggyBank();
        p.Init(1);
        gameManager.piggyBanks.Add(p);
        Chosen();
    }
    void PiggyBankPlus()
    {
        PiggyBank p = new PiggyBank();
        p.Init(3);
        gameManager.piggyBanks.Add(p);
        Chosen();
    }
    void SlowMode()
    {
        gameManager.handicap += 0.5f;
        Chosen();
    }
}

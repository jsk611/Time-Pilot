using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    List<string[]> upgradeTexts = new List<string[]>();

    // Start is called before the first frame update
    void Start()
    {
        upgradeTexts.Add(new string[] { "골든 기어", "타임머신 체력 +1" });
        upgradeTexts.Add(new string[] { "평범한 초시계", "이동속도 10 상승" });
        upgradeTexts.Add(new string[] { "전기태엽", "공격속도 10 상승" });
        upgradeTexts.Add(new string[] { "후방 발사기", "총알이 뒤로도 발사됩니다\n공격 속도가 15 감소" });
        upgradeTexts.Add(new string[] { "좌우 발사기", "총알이 좌우로도 발사됩니다\n공격 속도가 15 감소" });
        upgradeTexts.Add(new string[] { "보조 발사기", "보조 총알이 발사됩니다\n공격 속도가 30 감소" });
        upgradeTexts.Add(new string[] { "머신건", "공격 속도가 30 늘어나는 대신 \n이동 속도가 25% 감소합니다." });
        upgradeTexts.Add(new string[] { "기어 저금통", "다음 2번째 체크포인트에서 \n타임머신 체력 +1" });
        upgradeTexts.Add(new string[] { "기어 저금통 +", "다음 3번째 체크포인트에서 \n타임머신 체력 +2" });
        upgradeTexts.Add(new string[] { "슬로우 모드", "시간제한 스테이지에서\n제한시간 +0.5초" });
        //ResetChoice();


        Debug.Log(upgradeTexts[0][0]);
        Debug.Log(upgradeTexts[0][0].GetType());
        Debug.Log(upgradeTexts.Count);
    }
}

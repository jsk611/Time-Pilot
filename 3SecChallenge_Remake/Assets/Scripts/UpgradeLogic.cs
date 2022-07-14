using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeLogic : MonoBehaviour
{
    [SerializeField] Button[] upgradeBtns;
    [SerializeField] Player player;
    [SerializeField] Sprite[] imgs;
    string[,] upgradeTexts = new string[,]
    {
        { "골든 기어", "타임머신 체력 +1" },
    };
    // Start is called before the first frame update
    void Start()
    {
        int l = upgradeTexts.Length;

        for(int i=0; i<3; i++)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    List<string[]> upgradeTexts = new List<string[]>();

    // Start is called before the first frame update
    void Start()
    {
        upgradeTexts.Add(new string[] { "��� ���", "Ÿ�Ӹӽ� ü�� +1" });
        upgradeTexts.Add(new string[] { "����� �ʽð�", "�̵��ӵ� 10 ���" });
        upgradeTexts.Add(new string[] { "�����¿�", "���ݼӵ� 10 ���" });
        upgradeTexts.Add(new string[] { "�Ĺ� �߻��", "�Ѿ��� �ڷε� �߻�˴ϴ�\n���� �ӵ��� 15 ����" });
        upgradeTexts.Add(new string[] { "�¿� �߻��", "�Ѿ��� �¿�ε� �߻�˴ϴ�\n���� �ӵ��� 15 ����" });
        upgradeTexts.Add(new string[] { "���� �߻��", "���� �Ѿ��� �߻�˴ϴ�\n���� �ӵ��� 30 ����" });
        upgradeTexts.Add(new string[] { "�ӽŰ�", "���� �ӵ��� 30 �þ�� ��� \n�̵� �ӵ��� 25% �����մϴ�." });
        upgradeTexts.Add(new string[] { "��� ������", "���� 2��° üũ����Ʈ���� \nŸ�Ӹӽ� ü�� +1" });
        upgradeTexts.Add(new string[] { "��� ������ +", "���� 3��° üũ����Ʈ���� \nŸ�Ӹӽ� ü�� +2" });
        upgradeTexts.Add(new string[] { "���ο� ���", "�ð����� ������������\n���ѽð� +0.5��" });
        //ResetChoice();


        Debug.Log(upgradeTexts[0][0]);
        Debug.Log(upgradeTexts[0][0].GetType());
        Debug.Log(upgradeTexts.Count);
    }
}

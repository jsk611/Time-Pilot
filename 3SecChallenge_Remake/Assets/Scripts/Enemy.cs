using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    protected int hp;
    protected int maxHp;

    [SerializeField] protected Image hpBar;

    protected void SetHp(int maxHp)
    {
        this.maxHp = maxHp;
        hp = maxHp;
    }
    public void DecreaseHp()
    {
        hp--;
        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }
    protected void HpBar()
    {
        hpBar.fillAmount = (float)hp / maxHp;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("bullet"))
        {
            DecreaseHp();
        }
    }
}

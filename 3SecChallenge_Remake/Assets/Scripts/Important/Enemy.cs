using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    protected int hp;
    protected int maxHp;
    protected SpriteRenderer spr;

    [SerializeField] protected Image hpBar;

    protected void SetHp(int maxHp)
    {
        this.maxHp = maxHp;
        hp = maxHp;
        
    }
    public void DecreaseHp()
    {
        hp--;
        //StartCoroutine(Damaged());
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
    protected IEnumerator Damaged()
    {
        spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, 0.5f);
        yield return new WaitForSeconds(0.5f);
        spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, 1f);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    protected int hp;
    protected int maxHp;
    protected SpriteRenderer spr;
    bool isHit;
    bool isTutorial;
    protected bool isBoss;
    float t;
    [SerializeField] protected Image hpBar;
    [SerializeField] protected ParticleSystem damagedEvent;
    [SerializeField] protected GameObject destroyEvent;
    [SerializeField] protected AudioClip deathClip;
    GameManager gameManager;
    protected void SetHp(int maxHp)
    {
        this.maxHp = maxHp;
        hp = maxHp;
        spr = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
        if (spr == null)
            spr = GetComponentInChildren<SpriteRenderer>();

        if (gameManager == null)
            isTutorial = true;
    }
    public void DecreaseHp()
    {
        hp--;

        if (damagedEvent != null)
            StartCoroutine(Damaged(damagedEvent));
        else
            StartCoroutine(Damaged());
        
        if(hp <= 0)
        {
            if (destroyEvent != null)
                Instantiate(destroyEvent, transform.position, Quaternion.identity);

            if(!isTutorial) gameManager.sound.Play(deathClip, Define.Sound.Effect);
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
        if (collision.CompareTag("enemy") && isBoss)
        {
            DecreaseHp();
        }
    }
    protected IEnumerator Damaged()
    {
        spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, 0.75f);
        yield return new WaitForSeconds(0.15f);
        spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, 1f);

    }

    protected IEnumerator Damaged(ParticleSystem particle)
    {
        particle.Stop();
        particle.Play();

        spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, 0.75f);
        yield return new WaitForSeconds(0.15f);
        spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, 1f);

    }

}

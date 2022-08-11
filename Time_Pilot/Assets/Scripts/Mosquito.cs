using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mosquito : Enemy
{
    public GameObject player;
    [SerializeField] Transform childPos;
    [SerializeField] Animator anim;
    Rigidbody2D rigid;
    
    float targetX;
    float targetY;
    int rand;

    bool isMoving;

    void Start()
    {
        SetHp(5);
        rigid = GetComponent<Rigidbody2D>();
        StartCoroutine(StartMoving());
        //StartCoroutine(SetRandom());
        
    }

    // Update is called once per frame
    float tmpX;
    void Update()
    {
        HpBar();

        if(isMoving)
        {
            SetTarget();
            transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x, targetY), 1.5f*Time.deltaTime);
            transform.position = Vector2.Lerp(transform.position, new Vector2(targetX, transform.position.y), 1.5f*Time.deltaTime);

            
            if(Vector2.Distance(transform.position, new Vector2(targetX,targetY)) < 0.5f)
            {
                if (Random.Range(0, 2) == 0)
                    StartCoroutine(AttackRoutine());
                else
                    StartCoroutine(SetTarget());
            }

        }

        float x = transform.position.x;
        spr.flipX = tmpX - x < 0;
        tmpX = transform.position.x;
    }

    IEnumerator StartMoving()
    {
        Vector3 pos;
        if(transform.position.x > 0)
            pos = new Vector3(7.5f, transform.position.y,0);
        
        else
            pos = new Vector3(-7.5f, transform.position.y,0);

        while(transform.position != pos)
        {
            transform.position = Vector3.MoveTowards(transform.position, pos, 3.5f * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(SetTarget());
    }

    IEnumerator SetTarget()
    {
        isMoving = false;
        yield return new WaitForSeconds(1f);
        rand = Random.Range(-2, 3);
        targetX = player.transform.position.x + rand;
        targetY = player.transform.position.y + rand;
        if (targetY > 14f) targetY = 14f;
        else if (targetY < -4f) targetY = -4f;

        isMoving = true;
    }
    //IEnumerator SetRandom()
    //{
    //    while(true)
    //    {
    //        rand = Random.Range(-3, 4);
    //        yield return new WaitForSeconds(1f);
    //    }
    //}
    IEnumerator AttackRoutine()
    {
        isMoving = false;
        yield return new WaitForSeconds(1f);
        anim.SetBool("isAttacking", true);
        Debug.Log("Attack");
        Vector3 targetPos = player.transform.position;
        Vector3 dir = targetPos - transform.position;
        while(Mathf.Abs(transform.position.x) < 13f)
        {
            transform.Translate(dir.normalized * 11 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        //transform.position = new Vector2(tmpX, transform.position.y);
        anim.SetBool("isAttacking", false);
        yield return new WaitForSeconds(1f);
        StartCoroutine(SetTarget());
        
    }
}

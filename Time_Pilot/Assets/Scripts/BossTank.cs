using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTank : Enemy
{
    BossStage1Logic bossStage1Logic;

    bool pattern;
    [SerializeField] Transform[] pos;
    [SerializeField] GameObject sniper;
    // Start is called before the first frame update
    void Start()
    {
        bossStage1Logic = GetComponent<BossStage1Logic>();
        SetHp(50);
        StartCoroutine(StartPattern());
        isBoss = true;
    }

    // Update is called once per frame
    void Update()
    {
        HpBar();
    }

    IEnumerator StartPattern() 
    {
        yield return new WaitForSeconds(6f);
        while (transform.position.y > 5.5f) 
        {
            transform.Translate(Vector2.down * Time.deltaTime * 2f, Space.World);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(2f);
        StartCoroutine(MainPattern());
    }

    IEnumerator MainPattern() 
    {
        while(true)
        {
            if (!pattern) 
            {
                StartCoroutine("Pattern"+Random.Range(1,4).ToString());
                pattern = true;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator Pattern1() //돌진
    {
        yield return new WaitForEndOfFrame();
        while (transform.position.y > -8f)
        {
            transform.Translate(Vector2.down * Time.deltaTime * 7f, Space.World);
            yield return new WaitForEndOfFrame();
        }

        transform.position = pos[0].position;
        transform.rotation = pos[0].rotation;
        while (transform.position.x < 13f)
        {
            transform.Translate(Vector2.right * Time.deltaTime * 7f, Space.World);
            yield return new WaitForEndOfFrame();
        }

        transform.position = pos[1].position;
        transform.rotation = pos[1].rotation;
        while (transform.position.x > -13f)
        {
            transform.Translate(Vector2.left * Time.deltaTime * 7f, Space.World);
            yield return new WaitForEndOfFrame();
        }

        transform.position = pos[2].position;
        transform.rotation = pos[2].rotation;
        while (transform.position.y > 5.5f)
        {
            transform.Translate(Vector2.down * Time.deltaTime * 2f, Space.World);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(2f);
        pattern = false;
    }
    IEnumerator Pattern2() //발사
    {
        yield return new WaitForEndOfFrame();
        pattern = false;
    }
    IEnumerator Pattern3() //저격 소환
    {
        yield return new WaitForEndOfFrame();
        for(int i = 0; i< 3; i++)
        {
            Sniper s = Instantiate(sniper, new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f)), Quaternion.identity).GetComponent<Sniper>();
            s.player = FindObjectOfType<Player>().gameObject;
            Destroy(s.gameObject, 10f);
            yield return new WaitForSeconds(1f);
        }
        pattern = false;
    }


}

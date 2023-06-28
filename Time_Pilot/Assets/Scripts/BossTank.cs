using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BossTank : Enemy
{
    BossStage1Logic bossStage1Logic;
    public float bossHp;
    bool pattern;
    [SerializeField] Transform[] pos;
    [SerializeField] GameObject sniper;
    [SerializeField] GameObject bulletPrefab;
    float bulletSpeed = 8f; // �Ѿ� �ӵ�
    float fireRate = 5f; // �߻� �ӵ� (�ʴ� �߻� ��)
    float bulletSpread = 60f; // �Ѿ��� ������ ���� ���� (��)
    // Start is called before the first frame update
    void Start()
    {
        bossStage1Logic = GetComponent<BossStage1Logic>();
        SetHp(40);
        StartCoroutine(StartPattern());
        isBoss = true;
    }

    // Update is called once per frame
    void Update()
    {
        HpBar();
        bossHp = hp;
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
        int patternNum = 0;
        while(true)
        {
            if (!pattern) 
            {
                int randNum = Random.Range(1, 4);
                while (patternNum == randNum) randNum = Random.Range(1, 4);

                StartCoroutine("Pattern"+randNum.ToString());
                pattern = true;
                patternNum = randNum;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator Pattern1() //����
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
            transform.Translate(Vector2.right * Time.deltaTime * 12f, Space.World);
            yield return new WaitForEndOfFrame();
        }

        transform.position = pos[1].position;
        transform.rotation = pos[1].rotation;
        while (transform.position.x > -13f)
        {
            transform.Translate(Vector2.left * Time.deltaTime * 12f, Space.World);
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
    IEnumerator Pattern2() //�߻�
    {
        while (transform.position.y < 8.5f)
        {
            transform.Translate(Vector2.up * Time.deltaTime * 2f, Space.World);
            yield return new WaitForEndOfFrame();
        }
        int i = 0;
        while (i < 30)
        {
            // �߻� ���θ� �Ǵ��Ͽ� �Ѿ� �߻�
            // �Ѿ� ����
            GameObject bullet = Instantiate(bulletPrefab, transform.position + Vector3.down * 4, Quaternion.identity);

            // �Ѿ� ���� ����
            float angle = Random.Range(-bulletSpread, bulletSpread);
            Vector2 direction = Quaternion.Euler(0f, 0f, angle) * Vector2.down;
            bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
            bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle + 90f);
            i++;
            // �߻� �ӵ��� ���� ���
            yield return new WaitForSeconds(1f / fireRate);
        }
        while (transform.position.y > 5.5f)
        {
            transform.Translate(Vector2.down * Time.deltaTime * 2f, Space.World);
            yield return new WaitForEndOfFrame();
        }
        pattern = false;
    }
    IEnumerator Pattern3() //���� ��ȯ
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

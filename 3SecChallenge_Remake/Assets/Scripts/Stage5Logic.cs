using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5Logic : Stage
{
    [SerializeField] GameObject sniper;
    [SerializeField] GameObject tank;
    // Start is called before the first frame update
    void Start()
    {
        LoadInfo();
        SetTime(10f);
        StartCoroutine(ModernPattern());
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
        PlayerHit();
    }

    IEnumerator ModernPattern()
    {
        StartCoroutine(Sniper());
        yield return new WaitForSeconds(3f);
        StartCoroutine(Tank());
    }
    IEnumerator Sniper()
    {
        for(int i=0; i<4; i++)
        {
            Sniper s = Instantiate(sniper, new Vector2(Random.Range(-5f,5f), Random.Range(-5f,5f)), Quaternion.identity).GetComponent<Sniper>();
            s.player = player;
            yield return new WaitForSeconds(2f);

        }
    }
    IEnumerator Tank()
    {
        for(int i=0; i<3; i++)
        {
            int x;
            float randY = Random.Range(-4f, 4f);
            int randN = Random.Range(0, 2);
            if (randN == 0)
            {
                x = -11;
                Instantiate(tank, new Vector2(x, randY), Quaternion.identity);
            }
            else
            {
                x = 11;
                SpriteRenderer t = Instantiate(tank, new Vector2(x, randY), Quaternion.Euler(0,0,180)).GetComponent<SpriteRenderer>();
                t.flipY = true;
            }
            yield return new WaitForSeconds(2f);
        }


    }
}

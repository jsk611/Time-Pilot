using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4Logic : Stage
{
    [SerializeField] GameObject ball;
    [SerializeField] Transform[] spawns;
    void Start()
    {
        LoadInfo();
        StartCoroutine(SummonBalls());
        SetTime(10f);
    }

    private void Update()
    {
        Timer();
        PlayerHit();

    }
    IEnumerator SummonBalls()
    {
        yield return new WaitForSeconds(0.1f);
        for(int i=0; i<4; i++)
        {
            Instantiate(ball, spawns[i].position, Quaternion.identity);
            if (i != 3)
                Instantiate(ball, spawns[6 - i].position, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4Logic : MonoBehaviour
{
    [SerializeField] GameObject ball;
    [SerializeField] Transform[] spawns;
    void Start()
    {
        StartCoroutine(SummonBalls());
    }

    IEnumerator SummonBalls()
    {
        yield return new WaitForSeconds(0.3f);
        for(int i=0; i<4; i++)
        {
            Instantiate(ball, spawns[i].position, Quaternion.identity);
            if (i != 3)
                Instantiate(ball, spawns[6 - i].position, Quaternion.identity);
            yield return new WaitForSeconds(0.4f);
        }
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4Logic : Stage
{
    [SerializeField] GameObject ball;
    [SerializeField] Transform[] spawns;
    [SerializeField] GameObject cam;
    void Start()
    {
        LoadInfo();
        StartCoroutine(SummonBalls());
        SetTime(10f);

        if(Time.timeScale > 1.5f)
            StartCoroutine(RotateCam());
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
    IEnumerator RotateCam()
    {
        yield return new WaitForSeconds(4f);
        float a = 0;
        while(a <= 60f && time>0)
        {
            cam.transform.rotation = Quaternion.AngleAxis(a, Vector3.forward);
            a += Time.deltaTime * 40f;
            yield return new WaitForEndOfFrame();
        }
        //yield return new WaitForSeconds(0.5f);
        while (a >= -60f && time > 0)
        {
            cam.transform.rotation = Quaternion.AngleAxis(a, Vector3.forward);
            a -= Time.deltaTime * 40f;
            yield return new WaitForEndOfFrame();
        }
        //yield return new WaitForSeconds(0.5f);
        while (a <= 0f && time > 0)
        {
            cam.transform.rotation = Quaternion.AngleAxis(a, Vector3.forward);
            a += Time.deltaTime * 40f;
            yield return new WaitForEndOfFrame();
        }
        cam.transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
    }
    
}

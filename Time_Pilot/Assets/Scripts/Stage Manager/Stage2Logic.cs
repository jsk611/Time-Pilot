using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2Logic : Stage
{
    [SerializeField] GameObject cannon;
    [SerializeField] GameObject laserShooter;
    [SerializeField] Transform[] spawnPoints;
    // Start is called before the first frame update
    void Start()
    {
        LoadInfo();
        SetTime(12f);
        for (int i = 0; i < 6; i++)
        {
            int randNum = Random.Range(0, 3);
            if (randNum != 0)
            {
                GameObject g = Instantiate(cannon, spawnPoints[i].position, Quaternion.identity);
                g.GetComponent<Cannon>().player = player.transform;
                Destroy(g, 5f);
            }
        }
        StartCoroutine(Laser());
    }

    private void Update()
    {
        Timer();
        PlayerHit();
    }

    IEnumerator Laser()
    {
        yield return new WaitForSeconds(5f); 
        for(int i=0; i<18; i++)
        {
            Vector2 randPos = new Vector2(Random.Range(-8, 8), Random.Range(-4, 4));
            GameObject l = Instantiate(laserShooter, randPos, Quaternion.identity);
            l.GetComponent<LaserShooter>().player = player.transform;
            yield return new WaitForSeconds(0.3f);
        }
        
    }
}

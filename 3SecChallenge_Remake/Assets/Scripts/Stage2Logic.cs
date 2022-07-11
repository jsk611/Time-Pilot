using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2Logic : Stage
{
    [SerializeField] GameObject cannon;
    [SerializeField] Transform[] spawnPoints;
    // Start is called before the first frame update
    void Start()
    {
        SetTime(3f);
        LoadInfo();
        for (int i = 0; i < 7; i++)
        {
            int randNum = Random.Range(0, 3);
            if (randNum != 0)
            {
                GameObject g = Instantiate(cannon, spawnPoints[i].position, Quaternion.identity);
                g.GetComponent<Cannon>().player = player.transform;
                g.GetComponent<Cannon>().gameManager = gameManager;
            }
        }
    }

    private void Update()
    {
        Timer();
        PlayerHit();
    }
}

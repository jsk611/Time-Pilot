using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3Logic : Stage
{
    [SerializeField] GameObject target;
    [SerializeField] GameObject movingTarget;
    void Start()
    {
        LoadInfo();
        int r = Random.Range(1, 3);
        if(r==1)
        {
            for (int i = 0; i < 3; i++)
                Instantiate(target, new Vector2(Random.Range(-8, 8), Random.Range(-4, 4)), Quaternion.identity);
        }
        else
        {
            for (int i = 0; i < 2; i++)
            {
                float x = Random.Range(-8f, 8f);
                Instantiate(movingTarget, new Vector2(x, Random.Range(-4, 4)), Quaternion.identity);

            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.time < 0)
        {
            if (GameObject.FindGameObjectWithTag("target") == null)
                gameManager.Succeed();
            else
                gameManager.Failed(false);
        }
    }
}

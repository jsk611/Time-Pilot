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
                Instantiate(target, new Vector2(Random.Range(-8f, 8f), Random.Range(-4f, 4f)), Quaternion.identity);
        }
        else
        {
            for (int i = 0; i < 3; i++)
                Instantiate(movingTarget, new Vector2(Random.Range(-8f, -4f), Random.Range(-4f, 4f)), Quaternion.identity);
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

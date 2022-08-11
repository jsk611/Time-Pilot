using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage6Logic : Stage
{
    [SerializeField] GameObject arrow;
    [SerializeField] GameObject bomb;

    // Start is called before the first frame update
    void Start()
    {
        LoadInfo();
        SetTime(10f);
        StartCoroutine(SpawnArrow());
        StartCoroutine(SpawnBomb());
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
        PlayerHit();
    }
    IEnumerator SpawnArrow()
    {
        while(true)
        {
            int randNum = Random.Range(0, 2);
            float randX = randNum == 0 ? -9 : 9;
            float randY = Random.Range(0f, 4f);
            Vector2 spawnPos = new Vector2(randX, randY);
            float angle = randNum == 0 ? Random.Range(20f, 60f) : Random.Range(120f, 160f);
            Instantiate(arrow, spawnPos, Quaternion.Euler(0, 0, angle));
            yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
        }
    }
    IEnumerator SpawnBomb()
    {
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            int randNum = Random.Range(0, 2);
            float randX = randNum == 0 ? -9 : 9;
            float randY = Random.Range(0f, 4f);
            Vector2 spawnPos = new Vector2(randX, randY);
            float angle = randNum == 0 ? Random.Range(0f, 60f) : Random.Range(120f, 180f);
            Instantiate(bomb, spawnPos, Quaternion.Euler(0, 0, angle));
            yield return new WaitForSeconds(Random.Range(0.5f, 0.75f));
        }
    }
}

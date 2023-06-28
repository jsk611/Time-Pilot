using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class BossStage1Logic : Stage
{
    [SerializeField] RectTransform yearText;
    [SerializeField] Image fade;
    [SerializeField] BossTank bossTank;
    [SerializeField] GameObject bomb;
    // Start is called before the first frame update
    void Start()
    {
        LoadInfo();
        SetTime(45f);
        StartCoroutine(BiggerText(2f));
        StartCoroutine(SpawnBomb());
        //gameManager.Failed();
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
        PlayerHit();
        if (bossTank == null && time > 0.02f)
        {
            gameManager.Succeed();
            time = 0.01f;
        }
        if (time < 0)
        {
            gameManager.isInBossStage = false;
            if(bossTank != null) gameManager.Failed();

        }

    }
    IEnumerator BiggerText(float time)
    {
        yield return new WaitForSeconds(1f);
        float s = 0.5f;
        do
        {
            s += Time.deltaTime / time * 16;
            yearText.localScale = new Vector3(s, s, 1);
            yield return new WaitForEndOfFrame();
        } while (s < 16f);

        StartCoroutine(FadeIn(fade, 2f));


    }

    IEnumerator FadeIn(Image image, float time)
    {
        float a = 1;
        do
        {
            a -= Time.deltaTime / time;
            image.color = new Color(image.color.r, image.color.g, image.color.b, a);
            yield return new WaitForEndOfFrame();
        } while (image.color.a > 0);


    }

    IEnumerator SpawnBomb()
    {
        yield return new WaitForSeconds(10f);
        while (time > 1f)
        {
            int randNum = Random.Range(0, 2);
            float randX = randNum == 0 ? -9 : 9;
            float randY = Random.Range(0f, 4f);
            Vector2 spawnPos = new Vector2(randX, randY);
            float angle = randNum == 0 ? Random.Range(0f, 60f) : Random.Range(120f, 180f);
            Instantiate(bomb, spawnPos, Quaternion.Euler(0, 0, angle));
            yield return new WaitForSeconds(2f);
        }
    }

}

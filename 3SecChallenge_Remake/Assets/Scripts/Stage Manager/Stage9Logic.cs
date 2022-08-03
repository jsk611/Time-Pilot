using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage9Logic : Stage
{
    [SerializeField] Text[] stopT;
    [SerializeField] GameObject obstacle;
    [SerializeField] GameObject img;
    Vector3 vec;
    bool isStopping;
    // Start is called before the first frame update
    void Start()
    {
        LoadInfo();
        SetTime(20f);
        StartCoroutine(Stop());
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
        if(isStopping)
        {
            if (vec != player.transform.position)
            {
                gameManager.Failed();
                time = 0.001f;
                isStopping = false;
                img.SetActive(false);
            }
            if(time <= 0)
            {
                gameManager.Succeed();
                isStopping = false;
                foreach (var i in stopT)
                    i.gameObject.SetActive(false);
                img.SetActive(false);

            }
        }
    }

    IEnumerator Stop()
    {
        foreach(var i in stopT)
        {
            i.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.15f);
        }
        foreach(var i in stopT)
        {
            i.color = Color.red;
        }

        vec = player.transform.position;
        isStopping = true;
        img.SetActive(true);
        StartCoroutine(SummonObstacle());

        float a = 1f;
        while(stopT[0].color.a > 0.2f)
        {
            foreach (var i in stopT)
            {
                i.color = new Color(i.color.r, i.color.g, i.color.b, a);
                a -= 0.5f * Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
    }
    IEnumerator SummonObstacle()
    {
        while(time > 1f)
        {
            int rand = Random.Range(0, 2);
            if (rand == 0)
                Instantiate(obstacle, new Vector2(-10, Random.Range(4, -4)), Quaternion.identity);
            else
                Instantiate(obstacle, new Vector2(10, Random.Range(4, -4)), Quaternion.Euler(0,0,180));

            yield return new WaitForSeconds(0.75f);
        }

    }
}

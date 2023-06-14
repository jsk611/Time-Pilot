using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossStage1Logic : Stage
{
    [SerializeField] RectTransform yearText;
    [SerializeField] Image fade;
    // Start is called before the first frame update
    void Start()
    {
        //LoadInfo();
        SetTime(30f);
        StartCoroutine(BiggerText(4f));
    }

    // Update is called once per frame
    void Update()
    {
        //PlayerHit(true);
    }
    IEnumerator BiggerText(float time)
    {
        yield return new WaitForSeconds(3f);
        float s = 0.5f;
        do
        {
            s += Time.deltaTime / time * 15;
            yearText.localScale = new Vector3(s, s, 1);
            yield return new WaitForEndOfFrame();
        } while (s < 15f);

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

}

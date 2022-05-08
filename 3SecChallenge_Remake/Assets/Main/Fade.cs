using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public IEnumerator FadeIn(Image image, float time)
    {
        Debug.Log("aa");
        float a = 1;
        do
        {
            a -= Time.deltaTime / time;
            image.color = new Color(image.color.r, image.color.g, image.color.b, a);
            yield return new WaitForEndOfFrame();
        } while (image.color.a > 0);


    }

    public IEnumerator FadeOut(Image image, float time)
    {
        float a = 0;
        do
        {
            a += Time.deltaTime / time;
            image.color = new Color(image.color.r, image.color.g, image.color.b, a);
            yield return new WaitForEndOfFrame();
        } while (image.color.a < 1);
    }

    public IEnumerator FadeIn(SpriteRenderer spr, float time)
    {
        Debug.Log("aa");
        float a = 1;
        do
        {
            a -= Time.deltaTime / time;
            spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, a);
            yield return new WaitForEndOfFrame();
        } while (spr.color.a > 0);


    }

    public IEnumerator FadeOut(SpriteRenderer spr, float time)
    {
        float a = 0;
        do
        {
            a += Time.deltaTime / time;
            spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, a);
            yield return new WaitForEndOfFrame();
        } while (spr.color.a < 1);
    }
}

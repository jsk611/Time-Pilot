using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QArea : MonoBehaviour
{
    public SpriteRenderer spr;

    public bool isTriggered;


    // Start is called before the first frame update


    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Color color = spr.color;
            color.a = 0.8f;
            spr.color = color;

            isTriggered = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Color color = spr.color;
            color.a = 1f;
            spr.color = color;

            isTriggered = false;
        }
    }
}

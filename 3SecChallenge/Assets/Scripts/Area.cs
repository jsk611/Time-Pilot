using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    public SpriteRenderer spr;

    bool _isTriggered;

    public bool isTriggered
    {
        get
        {
            return _isTriggered;
        }
    }
    // Start is called before the first frame update


    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Color color = spr.color;
            color.a = 0.8f;
            spr.color = color;

            _isTriggered = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Color color = spr.color;
            color.a = 0.4f;
            spr.color = color;

            _isTriggered = false;
        }
    }
    

}

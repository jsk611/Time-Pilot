using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public SpriteRenderer spr;

    void Start()
    {

        Invoke("Activate", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Activate()
    {
        Color color = spr.color;
        color.a = 0.8f;
        spr.color = color;
        gameObject.tag = "Enemy";
    }


}

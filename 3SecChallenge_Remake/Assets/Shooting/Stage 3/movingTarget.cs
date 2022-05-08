using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingTarget : Target
{
    float speed;
    private void Start()
    {
        speed = Random.Range(3f, 8f);
    }
    void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * speed);
    }
}

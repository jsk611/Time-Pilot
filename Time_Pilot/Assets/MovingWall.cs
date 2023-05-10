using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall : MonoBehaviour
{
    float speed;
    Vector2 dir;
    // Start is called before the first frame update
    void Start()
    {
        speed = 2.5f;
        dir = Vector2.up;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.position.y > 1.25f)
            dir = Vector2.down;
        else if(transform.position.y < -1.25f)
            dir = Vector2.up;
        transform.Translate(dir * Time.fixedDeltaTime * speed);
    }
}

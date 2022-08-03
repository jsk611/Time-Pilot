using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : Enemy
{
    // Start is called before the first frame update
    [SerializeField] float speed;
    void Start()
    {
        SetHp(Random.Range(2, 7));
        speed = Random.Range(speed - 0.5f, speed + 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * speed);
    }
}

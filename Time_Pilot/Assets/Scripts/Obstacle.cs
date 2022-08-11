using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : Enemy
{
    // Start is called before the first frame update
    [SerializeField] float speed;
    void Start()
    {
        SetHp(Random.Range(1, 7));
        speed = 8f/hp;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * speed);
    }
}

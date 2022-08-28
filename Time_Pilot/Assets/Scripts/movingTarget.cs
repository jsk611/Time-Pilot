using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingTarget : Target
{
    float speed;
    public LayerMask layerMask;
    private void Start()
    {
        speed = transform.position.x <= 0 ? Random.Range(5f, 8f) : Random.Range(-8f,-5f);
        SetHp(1);
    }
    void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * speed);
    }

    private void FixedUpdate()
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, new Vector2(speed, 0), 1, layerMask);
        Debug.DrawRay(transform.position, new Vector2(speed, 0).normalized);
        if (hit.collider != null) speed *= -1;
    }
}

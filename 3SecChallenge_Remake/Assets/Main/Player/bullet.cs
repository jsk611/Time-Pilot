using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 5);
    }
    void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * 10);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            Destroy(gameObject);
    }
}
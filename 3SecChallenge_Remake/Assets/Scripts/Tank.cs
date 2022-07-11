using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    float speed;
    [SerializeField] GameObject particle;
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(Move());
        if (transform.rotation.z != 0)
            particle.transform.position = new Vector2(transform.position.x - 2f, transform.position.y -1.5f);

    }
    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
    IEnumerator Move()
    {
        speed = 0;
        yield return new WaitForSeconds(2f);
        speed = Random.Range(6f, 10f);

    }

}

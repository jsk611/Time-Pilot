using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : MonoBehaviour
{
    public GameObject player;
    [SerializeField] GameObject particle;
    [SerializeField] CircleCollider2D col;
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.Find("Player");
        StartCoroutine(MoveAndShoot());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed*Time.deltaTime);
    }

    IEnumerator MoveAndShoot()
    {
        while(true)
        {
            col.enabled = false;
            speed = Random.Range(4f, 6f);
            yield return new WaitForSeconds(1.5f);
            speed = 1f;
            yield return new WaitForSeconds(0.05f);
            col.enabled = true;
            Instantiate(particle, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
        }
    }
}

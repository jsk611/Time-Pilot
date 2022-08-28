using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : MonoBehaviour
{
    public GameObject player;
    [SerializeField] GameObject particle;
    [SerializeField] CircleCollider2D col;
    float speed;
    float x, y;

    [SerializeField] AudioClip clip;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();

        StartCoroutine(MoveAndShoot());
        StartCoroutine(RandomPos());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x + x, player.transform.position.y + y), speed*Time.deltaTime);
    }
    IEnumerator RandomPos()
    {
        while(true)
        {
            x = Random.Range(-1f, 1f);
            y = Random.Range(-1f, 1f);
            yield return new WaitForSeconds(0.25f);
        }
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
            audioSource.PlayOneShot(clip);
            col.enabled = true;
            Instantiate(particle, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
        }
    }
}

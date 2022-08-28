using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    float speed;
    [SerializeField] GameObject particle;

    [SerializeField] AudioClip clip;
    AudioSource audioSource;
    // Start is called before the first frame update
    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        StartCoroutine(Move());
        

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
        audioSource.PlayOneShot(clip);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShooter : MonoBehaviour
{
    public Transform player;
    public GameObject laser;
    float angle;
    [SerializeField] AudioClip clip;

    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        Vector2 dir = player.position - transform.position;

        // 타겟 방향으로 회전함
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        StartCoroutine(Rotating());
    }

    IEnumerator Rotating()
    {
        audioSource.PlayOneShot(clip);
        float a = 0;
        while(a<=360)
        {
            transform.rotation = Quaternion.AngleAxis(angle + a, Vector3.forward);
            a += Time.deltaTime * 720;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(0.4f);
        Instantiate(laser, transform.position, transform.rotation);
        
        Destroy(gameObject, 0.5f);
    }
}

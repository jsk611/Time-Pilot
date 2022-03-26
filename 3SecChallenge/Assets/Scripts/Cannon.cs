using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public Transform player;
    public SpriteRenderer spr;
    public GameObject bullet;
    public GameManager gameManager;
    public AudioSource audio;
    bool isDelay;
    float dTime;


    private void Start()
    {
        isDelay = true;
        dTime = Random.Range(0.7f, 1.2f);
        StartCoroutine("ShootCoroutine");
    }

    IEnumerator ShootCoroutine()
    {
        yield return new WaitForSeconds(dTime);
        isDelay = false;
        audio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector2 dir = player.position - transform.position;

        // 타겟 방향으로 회전함
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (Mathf.Abs(transform.eulerAngles.z) > 90 && Mathf.Abs(transform.eulerAngles.z) < 270)
            spr.flipY = true;
        else
            spr.flipY = false;

        if(!isDelay && !gameManager.isFinished)
        {
            isDelay = true;
            Instantiate(bullet, transform.position, transform.rotation);
            StartCoroutine("ShootCoroutine");
        }
    }

}

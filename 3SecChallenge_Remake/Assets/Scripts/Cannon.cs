using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public Transform player;
    public GameObject bullet;
    public GameManager gameManager;
    [SerializeField] Animator anim;

    bool isDelay;
    float dTime;


    private void Start()
    {

        isDelay = true;
        dTime = 1f;
        StartCoroutine(ShootCoroutine());
    }

    IEnumerator ShootCoroutine()
    {
        yield return new WaitForSeconds(dTime);
        isDelay = false;

    }

    // Update is called once per frame
    void Update()
    {

        Vector2 dir = player.position - transform.position;

        // 타겟 방향으로 회전함
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


        if (!isDelay)
        {
            isDelay = true;
            Instantiate(bullet, transform.position, transform.rotation);
            anim.SetTrigger("Shoot");
            StartCoroutine(ShootCoroutine());
        }
    }

}

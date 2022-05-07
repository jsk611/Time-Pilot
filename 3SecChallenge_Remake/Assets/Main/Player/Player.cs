using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Fade
{
    [SerializeField] GameManager gameManager;
    public float speed;
    [SerializeField] GameObject bullet;
    bool reloading;
    float angle;
    Vector2 mouse;
    [SerializeField] SpriteRenderer spr;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //이동
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        transform.Translate(new Vector2(x, y) * Time.deltaTime * speed, Space.World);

        //회전
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        angle = Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        //발사
        if (Input.GetMouseButton(0))
            StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        if (!reloading)
        {
            Instantiate(bullet, transform.position, transform.rotation);
            Debug.Log("발사");
            reloading = true;
            yield return new WaitForSeconds(0.5f);
            reloading = false;
        }
        else
            Debug.Log("장전중");
    }

    IEnumerator Damaged()
    {
        gameObject.tag = "DamagedPlayer";
        gameManager.Failed(true);
        for(int i=0; i<3; i++)
        {
            StartCoroutine(FadeIn(spr, 0.5f));
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(FadeOut(spr, 0.5f));
            yield return new WaitForSeconds(0.5f);
        }
        gameObject.tag = "Player";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("enemy") && gameObject.tag == "Player")
        {
            StartCoroutine(Damaged());
        }
    }
}

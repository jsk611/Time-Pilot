using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    public float speed;
    [SerializeField] GameObject bullet;
    bool reloading;
    float angle;
    float x, y;
    Vector2 mouse;
    [SerializeField] SpriteRenderer spr;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //이동
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        transform.Translate(new Vector2(x, y) * Time.deltaTime * speed, Space.World);

        //회전
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        angle = Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        //발사
        if (Input.GetMouseButton(0))
            StartCoroutine(Shoot());
    }
    private void FixedUpdate()
    {
        RaycastHit2D hit_x, hit_y;
        hit_x = Physics2D.Raycast(transform.position, new Vector2(x, 0),1,8);
        hit_y = Physics2D.Raycast(transform.position, new Vector2(0, y),1,8);
        if (hit_x.collider != null) x = 0;
        if (hit_y.collider != null) y = 0;


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
    public IEnumerator FadeIn(SpriteRenderer spr, float time)
    {
        Debug.Log("aa");
        float a = 1;
        do
        {
            a -= Time.deltaTime / time;
            spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, a);
            yield return new WaitForEndOfFrame();
        } while (spr.color.a > 0);


    }

    public IEnumerator FadeOut(SpriteRenderer spr, float time)
    {
        float a = 0;
        do
        {
            a += Time.deltaTime / time;
            spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, a);
            yield return new WaitForEndOfFrame();
        } while (spr.color.a < 1);
    }
    IEnumerator Damaged()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        string currentScene = SceneManager.GetActiveScene().name;
        gameObject.layer = 6;
        gameManager.Failed(true);
        while(currentScene == SceneManager.GetActiveScene().name)
        {
            StartCoroutine(FadeIn(spr, 0.5f));
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(FadeOut(spr, 0.5f));
            yield return new WaitForSeconds(0.5f);
        }
        gameObject.layer = 3;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("enemy"))
        {
            StartCoroutine(Damaged());
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        //�̵�
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        transform.Translate(new Vector2(x, y) * Time.deltaTime * speed, Space.World);

        //ȸ��
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        angle = Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        //�߻�
        if (Input.GetMouseButton(0))
            StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        if (!reloading)
        {
            Instantiate(bullet, transform.position, transform.rotation);
            Debug.Log("�߻�");
            reloading = true;
            yield return new WaitForSeconds(0.5f);
            reloading = false;
        }
        else
            Debug.Log("������");
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

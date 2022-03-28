using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    float speed;

    public GameManager gameManager;

    void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        speed = 4f;
        //DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (gameManager.isFinished)
        //    return;
        float h = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        float v = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;
        transform.Translate(h,v,0);
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            gameManager.isFailed = true;
            //gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!gameManager.isFinished && collision.gameObject.tag == "Enemy")
        {
            gameManager.isFailed = true;
            Destroy(collision.gameObject);
            //gameObject.SetActive(false);
        }
    }
}

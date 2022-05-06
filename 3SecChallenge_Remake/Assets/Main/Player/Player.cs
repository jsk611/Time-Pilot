using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    [SerializeField] GameObject bullet;
    bool reloading;
    float angle;
    Vector2 mouse;
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
}

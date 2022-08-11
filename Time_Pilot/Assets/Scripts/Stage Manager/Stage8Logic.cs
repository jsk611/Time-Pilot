using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage8Logic : Stage
{
    [SerializeField] GameObject cam;
    [SerializeField] GameObject meteo;
    [SerializeField] Animator wall;
    [SerializeField] Rigidbody2D rock;
    [SerializeField] float rockSpeed;
    [SerializeField] SpriteRenderer background;
    [SerializeField] Sprite img;
    bool cameraMoving;
    // Start is called before the first frame update
    void Start()
    {
        LoadInfo();
        SetTime(30f);
        Invoke("AddForceToRock", 2.5f);
        StartCoroutine(RotateCam1());
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
        PlayerHit();

        
    }
    private void LateUpdate()
    {
        if (cameraMoving)
            cam.transform.position = new Vector3(player.transform.position.x, cam.transform.position.y, -10);
        if (cam.transform.position.x >= 65 && cameraMoving)
        {
            cameraMoving = false;
            wall.SetTrigger("test");
            StartCoroutine(ResetPos());
        }
    }
    IEnumerator ResetPos()
    {
        background.sprite = img;
        yield return new WaitForSeconds(1f);
        StartCoroutine(RotateCam2());
        StartCoroutine(ZoomInOut());
        yield return new WaitForSeconds(1f);
        Destroy(wall.gameObject);
        cam.transform.position = new Vector3(0, 0, -10);
        player.transform.position = new Vector2(player.transform.position.x - 65f, player.transform.position.y);
        StartCoroutine(Meteo());
    }
    void AddForceToRock()
    {
        rock.AddForce(Vector2.right * 1000f * Time.deltaTime, ForceMode2D.Impulse);
        rock.velocity = Vector2.right * rockSpeed;
        cameraMoving = true;
    }
    IEnumerator RotateCam1()
    {
        yield return new WaitForSeconds(1f);
        float angle = 0f;
        while(angle<15f)
        {
            cam.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            angle += 15 * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

    }
    IEnumerator RotateCam2()
    {
        float angle = 15f;
        while (angle > 0f)
        {
            cam.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            angle -= 15 * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        cam.transform.rotation = Quaternion.Euler(0, 0, 0);

    }

    IEnumerator ZoomInOut()
    {
        Camera c = cam.GetComponent<Camera>();
        while(c.orthographicSize > 0)
        {
            c.orthographicSize -= 5 * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        while (c.orthographicSize < 5)
        {
            c.orthographicSize += 5 * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator Meteo()
    {
        yield return new WaitForSeconds(1f);
        while(true)
        {
            Instantiate(meteo, new Vector2(Random.Range(-6f, 6f), 7f), Quaternion.AngleAxis(Random.Range(-50f, 50f), Vector3.forward));
            yield return new WaitForSeconds(0.5f);
        }
    }
}

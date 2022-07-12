using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medieval : MonoBehaviour
{
    [SerializeField] bool isBomb;
    [SerializeField] GameObject BOOM;
    [SerializeField] float power;
    Rigidbody2D rigid;
    
    Vector2 curPos;
    Vector2 pastPos;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        float a = transform.rotation.eulerAngles.z;
        rigid.AddForce(new Vector2(Mathf.Cos(a*Mathf.Deg2Rad), Mathf.Sin(a*Mathf.Deg2Rad)).normalized * power * Time.deltaTime, ForceMode2D.Impulse);
        curPos = transform.position;
        pastPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        curPos = transform.position;
        Vector2 dir = curPos - pastPos;

        //스프라이트 랜더러 flipx 필요
        float angle = Mathf.Atan2(dir.y, dir.x)*Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        pastPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isBomb)
            Instantiate(BOOM, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

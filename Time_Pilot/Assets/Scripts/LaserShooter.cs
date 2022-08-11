using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShooter : MonoBehaviour
{
    public Transform player;
    public GameObject laser;
    float angle;
    // Start is called before the first frame update
    void Start()
    {
        Vector2 dir = player.position - transform.position;

        // Ÿ�� �������� ȸ����
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        StartCoroutine(Rotating());
    }

    IEnumerator Rotating()
    {
        float a = 0;
        while(a<=360)
        {
            transform.rotation = Quaternion.AngleAxis(angle + a, Vector3.forward);
            a += Time.deltaTime * 720;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(0.8f);
        Instantiate(laser, transform.position, transform.rotation);
        Destroy(gameObject, 0.5f);
    }
}
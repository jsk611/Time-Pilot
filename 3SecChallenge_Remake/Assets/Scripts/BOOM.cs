using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOOM : MonoBehaviour
{
    CircleCollider2D col;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<CircleCollider2D>();
        StartCoroutine(Umm());
    }
    IEnumerator Umm()
    {
        yield return new WaitForSeconds(0.2f);
        col.enabled = false;
    }
}

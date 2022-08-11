using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    [SerializeField] GameObject[] children;
    void Start()
    {
        StartCoroutine(LightningRoutine());
    }

    IEnumerator LightningRoutine()
    {
        yield return new WaitForSeconds(1f);
        children[0].SetActive(false);
        children[1].SetActive(true);
        Destroy(gameObject, 0.5f);
    }
}

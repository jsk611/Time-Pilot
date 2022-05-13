using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Count : MonoBehaviour
{
    [SerializeField] Image img; 
    void Update()
    {
        img.fillAmount -= Time.deltaTime / 0.5f;
        if (img.fillAmount <= 0)
            img.fillAmount = 1;
    }
}

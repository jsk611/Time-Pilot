using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : Enemy
{
    float z = 0;

    private void Start()
    {
        SetHp(16);
    }
    void Update()
    {
        z += Time.deltaTime * 180;
        transform.eulerAngles = new Vector3(0, 0, z);
    }
}

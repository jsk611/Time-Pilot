using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    float z=0;

    // Update is called once per frame
    void Update()
    {
        z += Time.deltaTime * 180;
        transform.eulerAngles = new Vector3(0, 0, z);
    }
}

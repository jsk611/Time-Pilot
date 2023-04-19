using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public bool isStarted;


    // Update is called once per frame
    void Update()
    {
        if(isStarted)
        {
            transform.Translate(Vector3.right * 5 * Time.timeScale * Time.deltaTime);
        }
    }
}

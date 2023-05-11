using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public bool isStarted;


    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector3.right * 5f * Time.deltaTime);
        if(Time.timeScale > 1.6f) transform.Translate(Vector3.right * 1f * Time.deltaTime);
        if(Time.timeScale >= 2f) transform.Translate(Vector3.right * 0.75f * Time.deltaTime);
    }
}

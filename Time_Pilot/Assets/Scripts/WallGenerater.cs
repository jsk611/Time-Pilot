using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGenerater : MonoBehaviour
{
    [SerializeField] GameObject wall;
    [SerializeField] GameObject movingWall;
    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < 12; i++)
        {
            Vector2 randPos = new Vector2(12 + i * 4f, Random.Range(-1f, 1f));
            Transform a = Instantiate(movingWall, randPos, Quaternion.identity).GetComponent<Transform>();
            a.localScale = new Vector2(0.5f, Random.Range(1.5f, 1.8f));
        }


    }

    
}

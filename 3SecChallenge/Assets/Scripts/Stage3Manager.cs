using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3Manager : MonoBehaviour
{
    public GameObject Laser;
    public Transform[] spawnPoints;
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<5; i++)
        {
            if (Random.Range(0, 3) != 0)
                Instantiate(Laser, spawnPoints[i].position, spawnPoints[i].rotation);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

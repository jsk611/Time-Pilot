using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5Manager : MonoBehaviour
{
    public GameObject player;
    public GameManager gameManager;
    public GameObject cannon;
    public Transform[] spawnPoints; 
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        for (int i=0; i<5; i++)
        {
            int randNum = Random.Range(0, 3);
            if(randNum != 0)
            {
                GameObject g = Instantiate(cannon, spawnPoints[i].position, Quaternion.identity);
                g.GetComponent<Cannon>().player = player.transform;
                g.GetComponent<Cannon>().gameManager = gameManager;
            }
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Logic : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] GameObject[] QAreas;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

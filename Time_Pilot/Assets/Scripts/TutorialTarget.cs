using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTarget : MonoBehaviour
{
    TutorialLogic tutorialLogic;
    // Start is called before the first frame update
    void Start()
    {
        tutorialLogic = FindAnyObjectByType<TutorialLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            tutorialLogic.mode1Num += 1;
            Destroy(gameObject);
        }
    }
}

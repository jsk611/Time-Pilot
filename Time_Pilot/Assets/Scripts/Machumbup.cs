using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Machumbup : Enemy
{
    public bool isAnswer;
    public TextMesh answerText;
    [SerializeField] GameObject WRONG;
    // Start is called before the first frame update
    void Start()
    {
        SetHp(3);
    }

    // Update is called once per frame
    void Update()
    {
        HpBar();
    }

    private void OnDestroy()
    {
        if (isAnswer && hp<=0)
            Instantiate(WRONG, transform.position, Quaternion.identity);
    }


}

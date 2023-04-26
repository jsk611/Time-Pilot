using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage10Logic : Stage
{
    [SerializeField] GameObject mosquito;
    [SerializeField] GameObject cam;

    [SerializeField] Transform[] points;
    [SerializeField] GameObject lightning;
    [SerializeField] GameObject lightningEffect;
    [SerializeField] AudioClip clip1;
    void Start()
    {
        LoadInfo();
        SetTime(30f);

        StartCoroutine(SpawnMosquito());
        StartCoroutine(SpawnLightning());
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
        PlayerHit();
    }
    private void LateUpdate()
    {
        float tmpY = player.transform.position.y;
        if (tmpY > 9.9f)
            tmpY = 9.9f;
        else if (tmpY < 0)
            tmpY = 0;
        cam.transform.position = new Vector3(0, tmpY, -10);
    }

    IEnumerator SpawnMosquito()
    {
        while(time > 0)
        {
            float x = Random.Range(0, 2) == 0 ? 11f : -11f; 
            float y = Random.Range(-4f, 14f);
            Mosquito m = Instantiate(mosquito, new Vector2(x, y), Quaternion.identity).GetComponent<Mosquito>();
            m.player = player;
            yield return new WaitForSeconds(4f);
        }
    }

    IEnumerator SpawnLightning()
    {
        yield return new WaitForSeconds(2f);
        while(time > 0)
        {
            foreach(var p in points)
            {
                int rand = Random.Range(0, 3);
                if (rand == 1)
                {
                    Instantiate(lightning, p.position, Quaternion.identity, cam.transform);
                }
            }
            yield return new WaitForSeconds(1f);
            gameManager.sound.Play(clip1, Define.Sound.Effect);
            lightningEffect.SetActive(false);
            lightningEffect.SetActive(true);
            yield return new WaitForSeconds(4f);
        }
    }
}

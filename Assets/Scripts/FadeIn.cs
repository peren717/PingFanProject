using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    Renderer rend;
    Color c;
    float FadeInTimer;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        c = rend.material.color;
        c.a = 0f;
        rend.material.color = c;
        FadeInTimer = 0.05f;
    }

    // Update is called once per frame
    void Update()
    {
        if (FadeInTimer >= 0)
        {
            FadeInTimer -= Time.deltaTime;
        }
        else
        {
            if (c.a < 1)
            {
                c.a += 0.1f;
                rend.material.color = c;
            }
            FadeInTimer = 1f;
        }


    }
}

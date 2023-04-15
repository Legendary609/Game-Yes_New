using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHole : MonoBehaviour
{
    SpriteRenderer spr;
    public float FadeDelay;
    public float FadeDuration;
    float timeElapsed;
    bool fading;
    // Start is called before the first frame update
    void Start()
    {
        spr = GetComponentInChildren<SpriteRenderer>();
        Invoke("Fade",FadeDelay);
    }

    // Update is called once per frame
    void Update()
    {
        if(!fading) return;
        spr.color = new Color(1,1,1,Mathf.Lerp(1f,0f,timeElapsed));
        timeElapsed += Time.deltaTime * FadeDuration;
        if(timeElapsed > 1)
        {
            Destroy(gameObject);
        }
    }
    void Fade()
    {
        fading = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunMaterialAnimation : MonoBehaviour
{
    public Material m;
    public float animationDuration;
    public float minOffset;
    public float maxOffset;
    float currentOffset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartAnimation()
    {
        StartCoroutine(AnimateOffset());
    }
    private IEnumerator AnimateOffset()
    {
        float elapsedTime = 0;
        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / animationDuration;
            currentOffset = Mathf.Lerp(minOffset, maxOffset, t);
            m.SetFloat("_Offset", currentOffset);
            yield return null;
        }
    }
}

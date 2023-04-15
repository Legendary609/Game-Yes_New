using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamagePopup : MonoBehaviour
{
    public float AnimationDelay;
    public TextMeshProUGUI text;
    public Animator anim;
    public int[] minimumDamage;
    public Color[] Colors; 
    RectTransform myTransform;
    Transform cam;
    public float defaultFontSize;
    public float distanceModifier;
    void Awake()
    {
        myTransform = GetComponent<RectTransform>();
        cam = GameObject.Find("MainCamera").GetComponent<Transform>();
    } 
    public void OnGUI()
    {
        Look();
    }
    public void SetDamage(int Damage,Vector3 hitPosition)
    {
        resetAnimation();
        anim.enabled = true;
        myTransform.position = hitPosition;
        text.text = Damage.ToString();
        for(int i = minimumDamage.Length; i > 0;i--)
        {
            if(Damage > minimumDamage[i - 1])
            {
                text.color = Colors[i - 1];
                break;
            }
        }
        Invoke("Animate",AnimationDelay);
    }
    void Animate()
    {
        anim.SetBool("Animating",true);
    }
    public void DestroyDamagePopup()
    {
        Destroy(gameObject);
    }
    void resetAnimation()
    {
        anim.SetBool("Animating",false);
    }
    void Look()
    {
        myTransform.LookAt(cam);
        float distance = Vector3.Distance(cam.transform.position, myTransform.position);
        float scaleFactor = distance * distanceModifier;
        text.fontSize = defaultFontSize * scaleFactor;
    }
}

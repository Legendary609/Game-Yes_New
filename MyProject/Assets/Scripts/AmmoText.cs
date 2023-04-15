using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AmmoText : MonoBehaviour
{
    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;

    void Start()
    {

        text1 = GameObject.Find("AmmoText1").GetComponent<TextMeshProUGUI>();
        text2 = GameObject.Find("AmmoText2").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateAmmoText(int AmmoLeft,int Ammo)
    {
        text1.text = AmmoLeft.ToString();
        text2.text = "/" + Ammo.ToString();
    }
}

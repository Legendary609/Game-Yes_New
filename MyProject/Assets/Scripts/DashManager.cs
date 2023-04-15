using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DashManager : MonoBehaviour
{
    public Slider slider;
    [HideInInspector]
    public float Value;
    [HideInInspector]
    public float MaximumValue;
    public float Amplifier;
    // Start is called before the first frame update
    void Start()
    {
        MaximumValue = slider.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        if(slider.value < MaximumValue)
        {
            slider.value += Time.deltaTime * Amplifier;
        }
        Value = slider.value;
    }
    public void ResetDash()
    {
        slider.value = 0f;
    }
}

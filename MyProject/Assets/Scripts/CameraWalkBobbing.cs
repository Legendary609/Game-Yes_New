using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWalkBobbing : MonoBehaviour
{
    public Transform cameraBobber;
    public float bobIntensity;
    public float bobSpeed;
    public float returnSpeed;
    public float shakeIntensity = 0.1f;
    public float shakeFrequency = 5f;
    float shakeOffset;

    // Start is called before the first frame update
    void Start()
    {
        shakeOffset = Random.Range(0f, Mathf.PI * 2);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputZ = Input.GetAxisRaw("Vertical");

        if(inputX != 0 || inputZ != 0 && !PlayerMovement.Sprinting)
        {
            Quaternion targetRotation = cameraBobber.localRotation * getRandomRotation();
            cameraBobber.localRotation = Quaternion.Slerp(cameraBobber.localRotation, targetRotation, bobSpeed);

            float shakeAmount = Mathf.Sin(Time.time * shakeFrequency + shakeOffset) * shakeIntensity;
            cameraBobber.localRotation *= Quaternion.Euler(shakeAmount, 0f, 0f);
        }
        else
        {
            cameraBobber.localRotation = Quaternion.Slerp(cameraBobber.localRotation, Quaternion.identity, returnSpeed);
        }
    }

    Quaternion getRandomRotation()
    {
        return Quaternion.Euler(Random.Range(-1f,1f) * bobIntensity, Random.Range(-1f,1f) * bobIntensity, 0f);
    }
}

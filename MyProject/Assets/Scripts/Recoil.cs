using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{
    public Transform Camera;

    Vector3 currentRotation;
    Vector3 targetRotation;
    public float snappiness;
    public float returnSpeed;
    void Start()
    {
    
    }
    void Update()
    {
        targetRotation = Vector3.Lerp(targetRotation,Vector3.zero,returnSpeed * Time.deltaTime);
        currentRotation = Vector3.Lerp(currentRotation,targetRotation,snappiness * Time.deltaTime);
        Camera.localRotation = Quaternion.Euler(currentRotation);
    }
    public void ApplyRecoil(float recoilX, float recoilY,float recoilZ)
    {
        targetRotation += new Vector3(recoilX,Random.Range(-recoilY,recoilY),Random.Range(-recoilZ,recoilZ));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Dash : MonoBehaviour
{
    //Mechanic
    public KeyCode DashKey;
    public Transform Camera;
    public DashManager dashManager;
    public float DashDistance;
    //Effects
    public float vignetteModifier;
    public float chromaticAberrationModifer;
    public float FovModifier;
    public float cameraShakeMagnitude;
    public float cameraShakeRoughness;
    public float cameraShakeDuration;
    float timeElapsed;
    public float lerpSpeed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(DashKey) && dashManager.Value == dashManager.MaximumValue)
        {
            DoDash();
        }
        if(timeElapsed > 0f)
        {
            if(PlayerMovement.Sprinting)
            {
                EffectsManager.effectsManager.ModifyVignette(Mathf.Lerp(EffectsManager.effectsManager.originalVignette,vignetteModifier,timeElapsed));
                EffectsManager.effectsManager.ModifyChromaticAberration(Mathf.Lerp(EffectsManager.effectsManager.originalChromaticAberration,chromaticAberrationModifer,timeElapsed));
            }
            else
            {
                EffectsManager.effectsManager.DoMainEffect
                (
                Mathf.Lerp(EffectsManager.effectsManager.originalVignette,vignetteModifier,timeElapsed)
                ,
                Mathf.Lerp(EffectsManager.effectsManager.originalChromaticAberration,chromaticAberrationModifer,timeElapsed)
                ,
                Mathf.Lerp(EffectsManager.effectsManager.originalFov,FovModifier,timeElapsed)
                );
            }

            timeElapsed -= Time.deltaTime * lerpSpeed;
        }
    }
    void DoDash()
    {
        EffectsManager.effectsManager.DoMainEffect(vignetteModifier,chromaticAberrationModifer,FovModifier);
        EffectsManager.effectsManager.ShakeCamera(cameraShakeMagnitude,cameraShakeRoughness,0.1f,cameraShakeDuration);
        RaycastHit hitpoint;
        if(Physics.Raycast(transform.position,Camera.forward,out hitpoint,DashDistance))
        {
            transform.position = hitpoint.point - (Camera.forward * 0.5f);
        }
        else
        {
            transform.position += Camera.forward * (DashDistance - 0.5f);
        }
        dashManager.ResetDash();
        timeElapsed = 1f;
    }
}

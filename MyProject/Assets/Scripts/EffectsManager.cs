using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using EZCameraShake;
public class EffectsManager : MonoBehaviour
{
    public static EffectsManager effectsManager;
    public VolumeProfile volume;
    private Vignette vignette;
    private ChromaticAberration chromaticAberration;
    public Camera cam;
    public Camera cam2;
    public float originalVignette;
    public float originalChromaticAberration;
    public float originalFov;
    // Start is called before the first frame update
    void Start()
    {
        if(effectsManager == null)
        {
            effectsManager = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        volume.TryGet<Vignette>(out vignette);
        volume.TryGet<ChromaticAberration>(out chromaticAberration);

        ResetEffects();
    }
    public void DoMainEffect(float vignetteModifier,float chromaticAberrationModifier,float FovModifier)
    {
        ModifyVignette(vignetteModifier);
        ModifyChromaticAberration(chromaticAberrationModifier);
        ModifyFov(FovModifier);
    }
    public void ModifyVignette(float vignetteModifier)
    {
        vignette.intensity.value = vignetteModifier;
    }
    public void ModifyChromaticAberration(float chromaticAberrationModifer)
    {
        chromaticAberration.intensity.value = chromaticAberrationModifer;
    }
    public void ModifyFov(float FovModifier)
    {
        cam.fieldOfView = FovModifier;
        cam2.fieldOfView = FovModifier;
    }
    public void ShakeCamera(float magnitude,float roughness,float fadeInTime,float shakeDuration)
    {
        CameraShaker.Instance.ShakeOnce(8f,6f,.1f,shakeDuration);
    }
    public void ResetEffects()
    {
        vignette.intensity.value = originalVignette;
        chromaticAberration.intensity.value = originalChromaticAberration;
        cam.fieldOfView = originalFov;
        cam2.fieldOfView = originalFov;
    }
}

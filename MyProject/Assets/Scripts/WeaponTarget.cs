using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTarget : MonoBehaviour
{
    public GameObject lastDamageEffect;
    public int lastDamageApplied;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ApplyDamageEffect(Vector3 hitPoint,int appliedDamage)
    {
        if(lastDamageEffect == null)
        {
            lastDamageApplied = 0;
            lastDamageEffect = CanvasManager.canvasManager.SpawnDamagePopup(hitPoint,appliedDamage + lastDamageApplied);
        }
        else
        {
            lastDamageEffect.GetComponent<DamagePopup>().SetDamage(appliedDamage + lastDamageApplied,hitPoint);
        }
        lastDamageApplied += appliedDamage;
    }
}

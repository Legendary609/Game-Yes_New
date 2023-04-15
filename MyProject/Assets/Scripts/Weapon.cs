using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Weapon : MonoBehaviour
{
    //Weapon Logic
    Transform Camera;
    public float FireRate;
    public float NumberOfBullets;
    public float Spread;
    bool canShoot = true;
    public bool Aiming;
    public float SpreadWhenNotAiming;
    public int Ammo;
    public int AmmoPerMagazine;
    public int AmmoInMagazine;
    public bool Continous;
    public KeyCode ReloadKey;
    public AmmoText ammoText;
    public LayerMask hitLayer;
    //Animation
    public Animator anim;
    public UnityEvent EventAfterShoot;
    //Effects
    public string EnemyLayer;
    public GameObject BulletHole;
    //Recoil
    Recoil recoil;
    [Range(0.0f, 20.0f)]
    public float RecoilX;
    [Range(0.0f, 20.0f)]
    public float RecoilY;
    [Range(0.0f, 20.0f)]
    public float RecoilZ;
    [Range(0.0f, 2f)]
    public float RecoilModifierWhenNotAiming;
    //Damage
    public int minimumDamage;
    public int maximumDamage;
    //Crosshair
    public Sprite crossHairSprite;
    public Vector2 crossHairSize;
    // Start is called before the first frame update
    void Start()
    {
        UpdateAmmo();
        Camera = GameObject.Find("RecoilCamera").GetComponent<Transform>();
        recoil = GetComponentInParent<Recoil>();
    }

    // Update is called once per frame
    void Update()
    {
        if(WeaponSwitchSystem.Switching) return;
        if(Continous)
        {
            if(Input.GetButton("Fire1") && canShoot && AmmoInMagazine > 0)
            {
                Shoot();
            }
        }
        else
        {
            if(Input.GetButtonDown("Fire1") && canShoot && AmmoInMagazine > 0)
            {
                Shoot();

            }
        }        
        
        if(Input.GetButton("Fire2"))
        {
            anim.SetBool("Aiming",true);
        }
        else
        {
            anim.SetBool("Aiming",false);
        }

        if(Input.GetKeyDown(ReloadKey) && Ammo > 0 && AmmoInMagazine < AmmoPerMagazine)
        {
            anim.SetTrigger("Reload");
        }
    }
    void Shoot()
    {
        for(int i = 0; i < NumberOfBullets; i++)
        {
            Vector3 shotDirection = Camera.forward;
            float Modifier = Spread + (Aiming ? 0f : SpreadWhenNotAiming);
            shotDirection = Quaternion.Euler(Random.Range(-Modifier,Modifier), Random.Range(-Modifier,Modifier), 0f) * shotDirection;
            RaycastHit hit;
            if(Physics.Raycast(Camera.position,shotDirection,out hit,Mathf.Infinity,hitLayer))
            {
                int appliedDamage = Random.Range(minimumDamage,maximumDamage);
                Transform hitTransform = hit.transform;
                int hitLayer = hitTransform.gameObject.layer;
                if(hitLayer == LayerMask.NameToLayer(EnemyLayer))
                {
                    hitTransform.GetComponent<WeaponTarget>().ApplyDamageEffect(hit.point,appliedDamage);
                    UnityEngine.Debug.Log("Hit An Enemy!");
                }
                else
                {
                    GameObject InstantiatedBulletHole = Instantiate(BulletHole,hit.point,Quaternion.identity);
                    InstantiatedBulletHole.transform.rotation = Quaternion.LookRotation(hit.normal);
                }
            }
        }
        AmmoInMagazine--;
        canShoot = false;
        Invoke("ResetShot",FireRate);
        anim.SetTrigger("Shoot");
        if(Aiming)  
        {
            recoil.ApplyRecoil(-RecoilX,RecoilY,RecoilZ);
        }
        else
        {
            recoil.ApplyRecoil(-RecoilX * RecoilModifierWhenNotAiming,RecoilY * RecoilModifierWhenNotAiming,RecoilZ * RecoilModifierWhenNotAiming);
        }
        UpdateAmmo();
        EventAfterShoot.Invoke();
    }
    public void Reload()
    {
        int ammoNeeded = AmmoPerMagazine - AmmoInMagazine;
        if(Ammo < ammoNeeded)
        {
            AmmoInMagazine += Ammo;
            Ammo = 0;
        }
        else
        {
            AmmoInMagazine += ammoNeeded;
            Ammo -= ammoNeeded;
        }
        UpdateAmmo();
    }
    public void ResetShot()
    {
        canShoot = true;
    }
    public void Aim()
    {
        Aiming = true;
    }
    public void UnAim()
    {
        Aiming = false;
    }
    public void UpdateAmmo()
    {
        ammoText.UpdateAmmoText(AmmoInMagazine,Ammo);
    }

    public void AddAmmo(int ammoToAdd)
    {

        Ammo += ammoToAdd;
        ammoToAdd = 15;

    }
    public void UpdateCrossHair()
    {
        CanvasManager.canvasManager.ChangeCrossHair(crossHairSprite,crossHairSize);
    }
}

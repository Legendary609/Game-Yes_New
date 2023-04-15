using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WeaponSwitchSystem : MonoBehaviour
{
    public static bool Switching;
    public List<Weapon> Weapons = new List<Weapon>();
    public int selectedWeapon;
    public Animator anim;
    public WeaponSlotSystem weaponSlotSystem;
    public LayerMask weaponItemLayer;
    public Transform cam;
    public float pickUpDistance;
    public KeyCode PickUpKey;
    GameObject objectToDestroy;
    // Start is called before the first frame update
    void Start()
    {
        switchWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit; 
        if(Physics.Raycast(cam.position,cam.forward,out hit,pickUpDistance,weaponItemLayer))
        {
            if(!CanvasManager.canvasManager.BottomMiddleTextEnabled)
            {
                CanvasManager.canvasManager.EnableBottomMiddleText("(" + PickUpKey.ToString() + ")" + " Pick up weapon",hit.point);
            }
            if(Input.GetKeyDown(PickUpKey))
            {
                Weapons.Add(hit.transform.GetComponent<WeaponItem>().WeaponObject);
                objectToDestroy = hit.transform.gameObject;
                selectedWeapon = Weapons.Count - 1;
                Switching = true;
                anim.SetTrigger("Switch");
            }
        }
        else
        {
            CanvasManager.canvasManager.DisableBottomMiddleText();
        }
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(selectedWeapon != 0)
            {
                selectedWeapon = 0;
                Switching = true;
                anim.SetTrigger("Switch");
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            if(selectedWeapon != 1)
            {
                selectedWeapon = 1;
                Switching = true;
                anim.SetTrigger("Switch");
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            if(selectedWeapon != 2)
            {
                selectedWeapon = 2;
                Switching = true;
                anim.SetTrigger("Switch");
            }
        }

        float scrollDelta = Input.mouseScrollDelta.y;
        if (scrollDelta > 0) 
        {
            if(selectedWeapon == Weapons.Count - 1)
            {
                selectedWeapon = 0;
            }
            else
            {
                selectedWeapon++;
            }
            Switching = true;
            anim.SetTrigger("Switch");
        } 
        else if (scrollDelta < 0) 
        {
            if(selectedWeapon == 0)
            {
                selectedWeapon = Weapons.Count - 1;
            }
            else
            {
                selectedWeapon--;
            }
            Switching = true;
            anim.SetTrigger("Switch");
        }        
    }
    public void switchWeapon()
    {
        if(objectToDestroy != null)
        {
            Destroy(objectToDestroy);
        }
        for(int i = 0; i < Weapons.Count ;i++)
        {
            Weapons[i].gameObject.SetActive(false);
        }
        Weapon weapon = Weapons[selectedWeapon];
        weapon.gameObject.SetActive(true);
        weapon.UpdateAmmo();
        weapon.UpdateCrossHair();
        Switching = false;
        weaponSlotSystem.ShowIcon(weapon.gameObject.name);
    }
}

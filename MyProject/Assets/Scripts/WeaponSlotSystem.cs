using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlotSystem : MonoBehaviour
{
    public List<GameObject> Icons = new List<GameObject>(); 
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void ShowIcon(string name)
    {
        foreach(GameObject icon in Icons)
        {
            if(icon.name == name)
            {
                icon.SetActive(true);
            }
            if(icon.name != name)
            {
                icon.SetActive(false);
            }
        }
    }
}

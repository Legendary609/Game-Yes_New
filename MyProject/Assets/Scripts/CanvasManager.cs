using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CanvasManager : MonoBehaviour
{
    public static CanvasManager canvasManager;
    public GameObject damagePopupObj;
    public RectTransform crossHair;
    public GameObject BottomMiddleGameObject;
    TextMeshProUGUI BottomMiddleText;
    [HideInInspector]
    public bool BottomMiddleTextEnabled = false;
    // Start is called before the first frame update
    void Start()
    {
        BottomMiddleText = BottomMiddleGameObject.GetComponent<TextMeshProUGUI>();
        canvasManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public GameObject SpawnDamagePopup(Vector3 hitPosition,int appliedDamage)
    {
        GameObject dmgPopup = Instantiate(damagePopupObj);
        dmgPopup.GetComponent<DamagePopup>().SetDamage(appliedDamage,hitPosition);
        return dmgPopup;
    }
    public void ChangeCrossHair(Sprite spr,Vector2 size)
    {
        crossHair.GetComponent<Image>().sprite = spr;
        crossHair.sizeDelta = size;
    }
    public void EnableBottomMiddleText(string text,Vector3 position)
    {
        BottomMiddleTextEnabled = true;
        BottomMiddleGameObject.SetActive(true);

        BottomMiddleText.text = text;
    }
    public void DisableBottomMiddleText()
    {
        BottomMiddleTextEnabled = false;
        BottomMiddleGameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class FollowCamera : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed;
    public Transform mainCamera;
    public float bobbingSpeed;
    public float offset;
    bool bobbing = false;
    public Vector3 Velocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    
        Quaternion newRotation = Quaternion.Euler( 
        transform.rotation.eulerAngles.x,
        player.rotation.eulerAngles.y, 
        transform.rotation.eulerAngles.z );
        transform.rotation = newRotation;
        //transform.position = Vector3.Lerp(transform.position,player.position,smoothSpeed * Time.deltaTime);
        transform.position = Vector3.SmoothDamp(transform.position,player.position,ref Velocity,smoothSpeed * Time.deltaTime);
    }
    public void BobDown()
    {
        if(bobbing) return;
        bobbing = true;
        Sequence sequence = DOTween.Sequence();
    
        sequence.Append(mainCamera.DOLocalMoveY(offset, bobbingSpeed))
        .Append(mainCamera.DOLocalMoveY(0f, bobbingSpeed))
        .OnComplete(() => bobbing = false);
    }
}

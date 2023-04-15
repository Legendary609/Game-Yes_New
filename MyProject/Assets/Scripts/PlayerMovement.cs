using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Movement
    Rigidbody rb;
    public float Speed;
    public float SprintSpeed;
    public float AimingSpeed;
    bool Aiming;
    public KeyCode SprintKey;
    public static bool Sprinting;
    float Modifier = 1f;
    //Jumping
    public Transform gCheck;
    public float gCheckRadius;
    public LayerMask groundLayer;
    bool onGround;
    public float jumpForce;
    public float modifierOnJump;
    //Sliding
    public KeyCode CrouchKey;
    bool Crouching;
    public Vector3 crouchScale;
    Vector3 originalScale;
    public float ModifierOnCrouch;
    public float SlideForce;
    public Transform Camera;
    public Camera camComponent;
    public float MouseSensitivity;
    float camRotation = 0f;
    float minimumSprintCameraZoom;
    public float maximumSprintCameraZoom;
    float timeElapsed = 0f;
    public float lerpSpeed;
    //Camera
    public FollowCamera followCamera;
    public float minimumBobbingVelocity;
    // Start is called before the first frame update
    void Start()
    {
        //Lock the cursor in the middle of the screen
        Cursor.lockState = CursorLockMode.Locked;
        //Disable the cursor from showing
        Cursor.visible = false;

        //Get the components
        rb = GetComponent<Rigidbody>();
        originalScale = transform.localScale;
        minimumSprintCameraZoom = EffectsManager.effectsManager.originalFov;
    }

    // Update is called once per frame
    void Update()
    {
        Look();
        Sprinting = Input.GetKey(SprintKey);
        Aiming = Input.GetButton("Fire2");
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");    
        float moveX = Speed * Modifier * (Sprinting ? SprintSpeed : 1f) * (Aiming ? AimingSpeed : 1f);
        float moveZ = Speed * Modifier * (Sprinting ? SprintSpeed : 1f) * (Aiming ? AimingSpeed : 1f);

        if(Input.GetKeyDown(CrouchKey))
        {
            Crouching = true;
        }
        if(Input.GetKeyUp(CrouchKey))
        {
            Crouching = false;
        }
        if(Crouching)
        {
            transform.localScale = crouchScale;
            if(onGround)
            {
                Modifier = ModifierOnCrouch;
            }
            if(!onGround)
            {
                moveZ += SlideForce;
            }
        }
        else
        {
            transform.localScale = originalScale;
        }

        Vector3 moveVector = new Vector3(moveX * inputX,rb.velocity.y,moveZ * inputZ);

        if (Mathf.Sqrt((Mathf.Pow(moveVector.x, 2) + Mathf.Pow(moveVector.z, 2))) > Speed * (Sprinting ? SprintSpeed : 1f) + ((Crouching && !onGround) ? SlideForce : 1f)) 
        {
            Vector3 n = moveVector.normalized * Speed;
            moveVector = new Vector3(n.x,moveVector.y,n.z);
        }
        rb.velocity = transform.TransformDirection(moveVector);

        if(Input.GetKey(KeyCode.Space) && onGround && !Crouching)
        {
            Jump();
        }
        if(!onGround)
        {
            Modifier = modifierOnJump;
        } 
        
        if(onGround && !Crouching)
        { 
            Modifier = 1f;
        }
        if(Sprinting && (inputX != 0f || inputZ != 0f) && rb.velocity.magnitude > 0f)
        {
            if(timeElapsed < 1f)
            {
                EffectsManager.effectsManager.ModifyFov(Mathf.Lerp(minimumSprintCameraZoom,maximumSprintCameraZoom,timeElapsed));
                timeElapsed += Time.deltaTime * lerpSpeed;
            }
        }
        else
        {
            if(timeElapsed > 0f)
            {
                EffectsManager.effectsManager.ModifyFov(Mathf.Lerp(minimumSprintCameraZoom,maximumSprintCameraZoom,timeElapsed));
                timeElapsed -= Time.deltaTime * lerpSpeed;
            }
        }
    }
    void FixedUpdate()
    {
        onGround = Physics.CheckSphere(gCheck.position,gCheckRadius,groundLayer);
    }
    void Look()
    {
        float mousex = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        float mousey = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * mousex);
        camRotation -= mousey;
        camRotation = Mathf.Clamp(camRotation,-90f,90f);
        
        Quaternion newRotation = Quaternion.Euler(camRotation,
        Camera.rotation.eulerAngles.y, 
        Camera.rotation.eulerAngles.z);
        Camera.rotation = newRotation;
    }
    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x,jumpForce,rb.velocity.z);
    }
    public void OnCollisionEnter(Collision other)
    {
        if(other.relativeVelocity.y > minimumBobbingVelocity && !Crouching)
        {
            followCamera.BobDown();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugPlayerController : MonoBehaviour
{
    public Transform camBody;
    public float sensitivity;
    public InputAction playerTriggerInput;
    public InputAction playerPointerPos;
    Camera cam;

    private void Awake()
    {
        playerTriggerInput.Enable();
        playerPointerPos.Enable();

        cam = Camera.main;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    bool TriggerInput
    {
        get
        {
            if (onPress && playerTriggerInput.ReadValue<float>() == 1)
            {
                onPress = false;
                return playerTriggerInput.ReadValue<float>() == 1;
            }
            if (playerTriggerInput.ReadValue<float>() == 0)
                onPress = true;
            return false;
        }
    }

    Vector2 MousePos => playerPointerPos.ReadValue<Vector2>();
    Vector3 ForwardDirection => cam.transform.forward;





    bool onPress;
    float _camRotation;

    private void Update()
    {
        ControlCamera();
    }

    private void ControlCamera()
    {
        float y = MousePos.y;
        float x = MousePos.x;

        _camRotation -= y * sensitivity * Time.deltaTime * 10f;
        _camRotation = Mathf.Clamp(_camRotation, -90f, 90f);

        cam.transform.localRotation = Quaternion.Euler(_camRotation, 0, 0);
        camBody.Rotate(x * sensitivity * Time.deltaTime * 10f * Vector3.up);
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, ForwardDirection * 100f, Color.red);
        if (TriggerInput && Physics.Raycast(transform.position, ForwardDirection, out hit))
        {
            NPC npc = hit.collider.GetComponent<NPC>();
            if (npc != null)
            {
                npc.EngageInDialog();
            }
        }
    }










}

/*
 
     public Transform characterBase;
    public float lookSpeed = 10f;

    private float _camRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float y = Input.GetAxis("Mouse Y");
        float x = Input.GetAxis("Mouse X");
        
        _camRotation -= y * lookSpeed * Time.deltaTime * 10f;
        _camRotation = Mathf.Clamp(_camRotation, -90f, 90f);
        
        transform.localRotation = Quaternion.Euler(_camRotation , 0f ,0f);
        
        characterBase.Rotate(x * lookSpeed * Time.deltaTime * 10f * Vector3.up);
        
        if(Input.GetMouseButton(0))
            Cursor.lockState = CursorLockMode.Locked;
        
        if(Input.GetKeyDown(KeyCode.Escape))
            Cursor.lockState = CursorLockMode.Confined;
    }*/

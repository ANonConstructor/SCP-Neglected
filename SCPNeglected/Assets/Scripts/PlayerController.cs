using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Code Variables")]
    private Rigidbody rb;
    private float mouseSens = 2f;
    private float walkSpeed = 250;
    private float xRotation = 0f;
    private float yRotation = 0f;

    [Header("UI Stuff")]
    public int playerHealth = 100;
    private UIScript UIScript;

    [Header("Interacting")]
    public Camera playerCamera;
    public LayerMask interactLayer;
    private float interactDistance = 5;

    [Header("Gun Variables")]
    private int damage = 5;
    public GameObject gun;
    [SerializeField] private bool hasGun = false;

    [Header("Animation")]
    private Animator gunAnimator;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = gameObject.GetComponent<Rigidbody>();
        UIScript = GameObject.Find("Canvas").GetComponent<UIScript>();
        gunAnimator = gun.GetComponent<Animator>();
    }

    void Update()
    {
        MouseMovement();
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }

        if(hasGun == true)
        {
            gunAnimator.SetFloat("Speed", rb.velocity.magnitude);
            if (Input.GetMouseButton(0))
            {
                Shooting();
            }
            else
            {
                NotShooting();
            }
        }
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void MouseMovement()
    {
        xRotation -= Input.GetAxisRaw("Mouse Y") * mouseSens;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        yRotation += Input.GetAxisRaw("Mouse X") * mouseSens;


        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }

    private void Movement()
    {
        Vector3 axis = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized * walkSpeed * Time.fixedDeltaTime;

        Vector3 forwardDirection = Quaternion.Euler(0, yRotation, 0) * Vector3.forward;
        Vector3 rightDirection = Quaternion.Euler(0, 90, 0) * forwardDirection;
        Vector3 wishDirection = (forwardDirection * axis.y + rightDirection * axis.x);

        rb.velocity = wishDirection;
    }
    private void Interact()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactDistance, interactLayer))
        {
            //Damage if it's an enemy and if it's a pickup, then destroy it
            if(hit.transform.gameObject.tag == "Pickup")
            {
                //Debug.Log(hit.transform.gameObject.name);
                switch (hit.transform.gameObject.name)
                {
                    case "SCP-127Pickup":
                        UIScript.AddItemToSlot(2, 1);
                        hasGun = true;
                        gun.SetActive(true);
                        break;
                    //case "HealthKit":
                        //UIScript.AddItemToSlot(, )
                        //break;
                    default:
                        Debug.Log("This pickup is named wrong or ");
                        break;
                }
                Destroy(hit.transform.gameObject);
                //Add code to add the pickup to the inventoru to inventory
            }
            else if(hit.transform.gameObject.tag == "Door")
            {
                //Stuff to do when it is a door
            }
        }
    }
    private void Shooting()
    {
        gunAnimator.SetBool("Shooting", true);
    }
    private void NotShooting()
    {
        gunAnimator.SetBool("Shooting", false);
    }
}

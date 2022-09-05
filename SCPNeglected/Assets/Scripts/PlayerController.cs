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
    public bool inInventory = false;

    [Header("Interacting")]
    public Camera playerCamera;
    public LayerMask interactLayer;
    private float interactDistance = 5;

    [Header("Gun Variables")]
    float fireElapsedTime = 0;
    private float fireDelay = 0.1f;
    private float gunRange = 10;
    private int damage = 5;
    public GameObject gun;
    public AudioClip gunSound;
    [SerializeField] private bool hasGun = false;
    public LayerMask enemyLayer;

    [Header("Animation")]
    private Animator gunAnimator;

    [Header("Sound")]
    public AudioSource playerSoundSrc;

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
        Interact();

        if (hasGun == true)
        {
            gunAnimator.SetFloat("Speed", rb.velocity.magnitude);
            if (inInventory == false)
            {
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
        Vector3 axis = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * walkSpeed * Time.fixedDeltaTime;

        Vector3 forwardDirection = Quaternion.Euler(0, yRotation, 0) * Vector3.forward;
        Vector3 rightDirection = Quaternion.Euler(0, 90, 0) * forwardDirection;
        Vector3 wishDirection = (forwardDirection * axis.y + rightDirection * axis.x);

        rb.velocity = wishDirection;
    }
    private void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactDistance, interactLayer))
            {
                if (hit.transform.gameObject.tag == "Pickup")
                {
                    //Debug.Log(hit.transform.gameObject.name);
                    switch (hit.transform.gameObject.name)
                    {
                        case "SCP-127Pickup":
                            UIScript.AddItemToSlot(1, 0);
                            hasGun = true;
                            gun.SetActive(true);
                            break;
                        case "MedkitPickup":
                            UIScript.AddItemToSlot(3, 1);
                            break;
                        default:
                            Debug.Log("This pickup is named wrong or ");
                            break;
                    }
                    Destroy(hit.transform.gameObject);
                    //Add code to add the pickup to the inventoru to inventory
                }
                else if (hit.transform.gameObject.tag == "Door")
                {
                    //Stuff to do when it is a door
                }
            }
        }
    }
    private void Shooting()
    {
        gunAnimator.SetBool("Shooting", true);
    
        fireElapsedTime += Time.deltaTime;

        if(fireElapsedTime >= fireDelay && gunAnimator.GetCurrentAnimatorStateInfo(0).IsName("Armature|Shooting") || fireElapsedTime >= fireDelay && gunAnimator.GetCurrentAnimatorStateInfo(0).IsName("Armature|ShootContinous"))
        {
            fireElapsedTime = 0;

            playerSoundSrc.PlayOneShot(gunSound);

            RaycastHit hit;
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, gunRange, enemyLayer))
            {
                Debug.Log(hit.transform.gameObject.name);
                //if (hit.transform.gameObject.tag == "SCP NAME HERE")
                //{ 
                //    //do stuff here
                //}
           }
           
        }

    }
    private void NotShooting()
    {
        gunAnimator.SetBool("Shooting", false);
    }
}

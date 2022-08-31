using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public GameObject[] slots;
    public Texture[] itemSprites;
    private GameObject inventory;
    private GameObject crosshair;
    [HideInInspector] public TMP_Text healthText;
    private PlayerController playerController;

    private void Start()
    {
        inventory = GameObject.Find("Inventory");
        crosshair = GameObject.Find("Crosshair");
        healthText = GameObject.Find("Health").GetComponent<TMP_Text>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        healthText.text = "Health: " + playerController.playerHealth;
    }
    private void Update()
    {

        if (Input.GetKey(KeyCode.Tab))
        {
            playerController.inInventory = true;
            inventory.SetActive(true);
            crosshair.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            playerController.inInventory = false;
            inventory.SetActive(false);
            crosshair.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void AlterHealth(int amountOfChange)
    {
        playerController.playerHealth += amountOfChange;
        playerController.playerHealth = Mathf.Clamp(playerController.playerHealth, 0, 100);
        healthText.text = "Health: " + playerController.playerHealth;
    }

    public void AddItemToSlot(int slotNumber, int spriteNumber)
    {
        slots[slotNumber].GetComponent<RawImage>().texture = itemSprites[spriteNumber];
    }

    //Button Methods (Don't touch)
    public void InventoryInteractButton(RawImage iconComponent)
    {
        switch (iconComponent.texture.name)
        {
            case "MedkitTexture":
                AlterHealth(50);
                break;
            case "HardWaterTexture":
                break;
            default:
                Debug.Log("The texture for this item is incorrectly named");
                break;
        }
        iconComponent.texture = null;
    } 
    public void InventoryDropButton(RawImage iconComponent)
    {
        switch (iconComponent.texture.name)
        {
            case "SCP-127Texture":
                
                break;
            case "MedkitTexture":
                break;
            case "HardWaterTexture":
                break;
            default:
                Debug.Log("The texture for this item is incorrectly named");
                break;
        }
        iconComponent.texture = null;
    }
}

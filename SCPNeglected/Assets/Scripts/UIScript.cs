using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    private GameObject inventory;
    private GameObject crosshair;
    private TMP_Text healthText;
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
        inventory.SetActive(true);
        crosshair.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
      }
      else
      {
        inventory.SetActive(false);
        crosshair.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
      }
    }

    public void AlterHealth(int amountOfChange)
    {
        playerController.playerHealth += amountOfChange;
        healthText.text = "Health: " + playerController.playerHealth;
    }
}

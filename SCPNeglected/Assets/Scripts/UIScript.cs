using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    private GameObject inventory;
    private TMP_Text healthText;
    private PlayerController playerController;

    private void Start()
    {
        inventory = GameObject.Find("Inventory");
        healthText = GameObject.Find("Health").GetComponent<TMP_Text>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        healthText.text = "Health: " + playerController.playerHealth;
    }
    private void Update()
    {
      if (Input.GetKey(KeyCode.Tab))
      {
        inventory.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
      }
      else
      {
        inventory.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
      }
    }

    public void AlterHealth(int amountOfChange)
    {
        playerController.playerHealth += amountOfChange;
        healthText.text = "Health: " + playerController.playerHealth;
    }
}

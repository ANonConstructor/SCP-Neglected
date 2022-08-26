using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleTabScrit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
             gameObject.GetComponent<Canvas> ().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetKey(KeyCode.Tab))
      {
        gameObject.GetComponent<Canvas> ().enabled = true;
        Cursor.lockState = CursorLockMode.None;
      }
      else
      {
        gameObject.GetComponent<Canvas> ().enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
      }
    }
}

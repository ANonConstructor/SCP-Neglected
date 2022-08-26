using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgPlayerOnTouch : MonoBehaviour
{
    private UIScript UIScript;
    public int damageToDeal;
    void Start()
    {
        UIScript = GameObject.Find("Canvas").GetComponent<UIScript>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        UIScript.AlterHealth(damageToDeal);
    }
}

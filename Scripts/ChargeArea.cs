using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeArea : MonoBehaviour
{
    [SerializeField] private GameObject chargeButtonObject;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            chargeButtonObject.SetActive(true);
        }   
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            chargeButtonObject.SetActive(false);    
        }

    }

    public void Charge()
    {
        SoldierChickenSquad.Instance.Charge();
        chargeButtonObject.SetActive(false);
    }
}

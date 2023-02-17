using System;
using System.Collections;
using System.Collections.Generic;
using Base.Runtime.Manager;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeArea : MonoBehaviour
{
    [SerializeField] private float interval = 1f;
    [SerializeField] private Image counterFillImage;
    private float _setInterval = 1f;
    private bool _isEnabled = false;

    private void Start()
    {
        _setInterval = interval;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interval = _setInterval;
            counterFillImage.fillAmount = 0f;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")&& !_isEnabled)
        {
            interval -= Time.deltaTime;
            counterFillImage.fillAmount = (_setInterval-interval)/_setInterval;
            if (interval <= 0 )
            {
                _isEnabled = true;
                counterFillImage.fillAmount = 1f;
                InterfaceManager.Instance.EnableUpgradePanel();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InterfaceManager.Instance.DisableUpgradePanel();
            interval = _setInterval;
            _isEnabled = false;
            counterFillImage.fillAmount = 0f;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockInPosition : MonoBehaviour
{
    private Vector3 _startPosition;
    void Start()
    {
        _startPosition = transform.position;
    }


    private void LateUpdate()
    {
        transform.position = _startPosition;
    }
}

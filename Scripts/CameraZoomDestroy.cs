using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraZoomDestroy : MonoBehaviour
{
    public float maxCamDistance = 40f;
    public float minCamFOV = 5f;
    public float fovSpeed = 0.2f;

    public Transform target;
    public Camera cam;

    float initialFOV;
    // Start is called before the first frame update
    void Start()
    {
       cam = this.GetComponent<Camera>();
       initialFOV = cam.fieldOfView;
    }

    //void ResetFOV()
   // {
       // cam.fieldOfView = initialFOV;
   // }
    // Update is called once per frame
    void LateUpdate()
    {
        if(target == null)
        {
            cam.fieldOfView = maxCamDistance;
        }
    }
}

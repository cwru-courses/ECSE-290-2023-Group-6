using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform followTransform;
    public float smoothSpeed = 0.5f;
    public float lowerBound = 0;

    private Camera mainCam;
    private Vector3 newPos;

    void Start()
    {
        mainCam = GetComponent<Camera>();
    }
    
    void Update()
    {
        newPos = new Vector3(followTransform.position.x, Math.Max(followTransform.position.y, lowerBound), this.transform.position.z);
        this.transform.position = Vector3.Lerp(this.transform.position, newPos, smoothSpeed);
    }
}

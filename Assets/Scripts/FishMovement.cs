using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public float maxLaunchForce = 100f;
    
    private Vector2 clickStart = Vector2.zero;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        } else if (Input.GetMouseButtonUp(0))
        {
            Vector2 clickEnd = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = clickEnd - clickStart;
            GetComponent<Rigidbody2D>().AddForce(direction * maxLaunchForce);
        }
    }
}

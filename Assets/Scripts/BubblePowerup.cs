using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblePowerup : MonoBehaviour
{

    public float refillAmount = 0.5f;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            WaterMeter.instance.AddWater(refillAmount);
            if (SoundManager.instance != null)
                SoundManager.instance.Pop();
            Destroy(this.gameObject);
        }
    }
}

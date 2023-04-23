using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterMeter : MonoBehaviour
{
    public static WaterMeter instance;
    public Image waterMeter;
    public float waterLossRate = 0.1f;
    private float realValue = 1f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        realValue -= waterLossRate * Time.deltaTime;
        waterMeter.fillAmount = realValue;

        if (realValue <= 0f)
        {
            LevelManager.instance.Lose();
        }
    }

    public void AddWater(float amount)
    {
        realValue += amount;
        if (realValue > 1f)
        {
            realValue = 1f;
        }
    }

    public void SubtractWater(float amount)
    {
        realValue -= amount;
        if (realValue < 0f)
        {
            realValue = 0f;
        }
    }

    public float GetWater()
    {
        return realValue;
    }
}

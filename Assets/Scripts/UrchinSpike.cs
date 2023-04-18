using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UrchinSpike : MonoBehaviour
{
    private PointEffector2D grip;
    private float gripForce;
    private bool isGripping = false;
    private UrchinSpike urchinSpike;

    IEnumerator WaitToDisengage(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Disengage();
        isGripping = false;
    }

    void Start()
    {
        grip = GetComponent<PointEffector2D>();
        gripForce = grip.forceMagnitude;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !isGripping)
        {
            Engage();
            isGripping = true;
        }
    }

    public void Engage()
    {
        grip.forceMagnitude = gripForce;
        StartCoroutine(WaitToDisengage(5f));
    }

    public void Disengage()
    {
        grip.forceMagnitude = 0;
    }
}

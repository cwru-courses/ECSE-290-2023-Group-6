using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UrchinSpike : MonoBehaviour
{
    public float gripTime = 3f;
    public float cooldown = 2f;

    private PointEffector2D grip;
    private float gripForce;
    private bool isGripping = false;

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
        StartCoroutine(WaitToDisengage());
    }

    IEnumerator WaitToDisengage()
    {
        yield return new WaitForSeconds(gripTime);
        Disengage();
        yield return new WaitForSeconds(cooldown);
        isGripping = false;
    }

    public void Disengage()
    {
        grip.forceMagnitude = 0;
    }
}

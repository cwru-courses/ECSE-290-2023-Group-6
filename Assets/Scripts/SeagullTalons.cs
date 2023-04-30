using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeagullTalons : MonoBehaviour
{
    private SeagullController seagull;
    private PointEffector2D grip;
    private float gripForce;
    private float engageThreshold = 0.1f;

    void Start()
    {
        seagull = GetComponentInParent<SeagullController>();
        grip = GetComponent<PointEffector2D>();
        gripForce = grip.forceMagnitude;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && seagull.state == SeagullController.State.attacking)
        {
            seagull.state = SeagullController.State.carrying;
        }
    }

    public void Engage() {
        grip.forceMagnitude = gripForce;
    }

    public void Disengage() {
        grip.forceMagnitude = 0;
    }
}

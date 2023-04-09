using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeagullTalons : MonoBehaviour
{
    private SeagullController seagull;
    private PointEffector2D grip;
    private float gripForce;

    void Start()
    {
        seagull = GetComponentInParent<SeagullController>();
        grip = GetComponent<PointEffector2D>();
        gripForce = grip.forceMagnitude;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && seagull.state == SeagullController.State.attacking)
        {
            seagull.curTarget = seagull.AroundHome();
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

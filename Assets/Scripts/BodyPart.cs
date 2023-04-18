using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floppable")
        {
            FishMovement.instance.AllowLaunch();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (SoundManager.instance != null && collision.gameObject.tag == "Floppable")
        {
            SoundManager.instance.Flop(collision.relativeVelocity.magnitude);
        }
    }
}

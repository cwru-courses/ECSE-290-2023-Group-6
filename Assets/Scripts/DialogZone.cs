using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogZone : MonoBehaviour
{
    public string speaker;
    public string text;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            DialogManager.instance.Say(speaker, text);
            Destroy(this.gameObject);
        }
    }
}

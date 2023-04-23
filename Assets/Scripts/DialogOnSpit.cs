using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogOnSpit : MonoBehaviour
{
    public string dialogName;
    public string dialogText;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Spit")
        {
            DialogManager.instance.Say(dialogName, dialogText);
        }
    }
}

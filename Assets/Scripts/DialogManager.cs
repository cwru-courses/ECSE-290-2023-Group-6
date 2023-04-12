using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;

    public Sprite[] speakerSprites;
    public string[] speakerNames;
    public GameObject dialogPrefab;

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

    public void Say(string name, string text)
    {
        GameObject dialog = Instantiate(dialogPrefab, transform);
        dialog.transform.parent = transform;
        dialog.GetComponent<DialogBox>().speakerImage.sprite = speakerSprites[System.Array.IndexOf(speakerNames, name)];
        dialog.GetComponent<DialogBox>().text.text = text;
    }
}

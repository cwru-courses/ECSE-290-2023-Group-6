using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public Sprite[] speakerSprites;
    public string[] speakerNames;
    public GameObject dialogPrefab;

    public void Say(string name, string text)
    {
        GameObject dialog = Instantiate(dialogPrefab, transform);
        dialog.GetComponentInChildren<Image>().sprite = speakerSprites[System.Array.IndexOf(speakerNames, name)];
        dialog.GetComponentInChildren<Text>().text = text;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneZone : MonoBehaviour
{
    public List<string> speakers;
    public List<string> lines;
    public List<Transform> focusObjects;
    public float timeBetweenLines = 4f;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(PlayCutscene());
        }
    }

    IEnumerator PlayCutscene()
    {
        FishMovement.instance.allowInput = false;
        for (int i = 0; i < speakers.Count; i++)
        {
            DialogManager.instance.Say(speakers[i], lines[i]);
            if (focusObjects[i])
            {
                Camera.main.GetComponent<PlayerCamera>().followTransform = focusObjects[i];
            }
            yield return new WaitForSeconds(timeBetweenLines);
        }
        FishMovement.instance.allowInput = true;
        Destroy(gameObject);
        Camera.main.GetComponent<PlayerCamera>().followTransform = FishMovement.instance.transform;
    }
}

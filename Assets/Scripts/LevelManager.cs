using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public TextMeshProUGUI timerText;

    private float timeElapsed = 0f;
    private bool isStopped = false;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
    }

    public void Update() {
        timeElapsed += isStopped ? 0 : Time.deltaTime;
        timerText.text = TimeText();
    }

    public string TimeText() {
        return $"{timeElapsed/60:00}:{timeElapsed%60:00}";
    }

    public void StopTimer() {
        isStopped = true;
    }
}

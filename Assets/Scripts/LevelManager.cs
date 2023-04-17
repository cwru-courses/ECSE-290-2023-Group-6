using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public TextMeshProUGUI timerText;
    public GameObject winScreen;
    public GameObject loseScreen;

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
        WaterMeter.instance.waterLossRate = 0;
    }

    public void Win() {
        StopTimer();
        winScreen.SetActive(true);
        FishMovement.instance.allowInput = false;
        Debug.Log("You win!");
    }

    public void Lose() {
        StopTimer();
        loseScreen.SetActive(true);
        FishMovement.instance.allowInput = false;
        Debug.Log("You lose!");
    }

    public void GoToScene(string sceneName) {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void Restart() {
        GoToScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void AddTime()
    {
        timeElapsed += 15f;
    }
}

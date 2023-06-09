using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public int switchToBgmIndex = -1;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI bestTimeText;
    public GameObject winScreen;
    public GameObject loseScreen;

    private float timeElapsed = 0f;
    private bool isStopped = false;
    private string CurrentSceneName() => UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
    }

    public void Start() {
        if (PlayerPrefs.HasKey(CurrentSceneName() + "_BestTime") && bestTimeText)
            bestTimeText.text = "Best " + TimeText(PlayerPrefs.GetFloat(CurrentSceneName() + "_BestTime"));
        else if (bestTimeText)
            bestTimeText.text = "Best --:--";

        if (switchToBgmIndex >= 0)
            SoundManager.instance.PlayMusic(switchToBgmIndex);
    }

    public void Update() {
        if (!timerText) return;
        timeElapsed += isStopped ? 0 : Time.deltaTime;
        timerText.text = TimeText(timeElapsed);
    }

    public string TimeText(float time) {
        return $"{time/60:00}:{time%60:00}";
    }

    public void StopTimer() {
        isStopped = true;
        WaterMeter.instance.waterLossRate = 0;
    }

    public void Win() {
        if (winScreen.activeSelf || loseScreen.activeSelf) return;
        StopTimer();
        winScreen.SetActive(true);
        FishMovement.instance.allowInput = false;

        if (PlayerPrefs.GetFloat(CurrentSceneName() + "_BestTime", Mathf.Infinity) > timeElapsed)
            PlayerPrefs.SetFloat(CurrentSceneName() + "_BestTime", timeElapsed);

        SoundManager.instance.Win();
        Debug.Log("You win!");
    }

    public void Lose() {
        if (winScreen.activeSelf || loseScreen.activeSelf) return;
        StopTimer();
        loseScreen.SetActive(true);
        FishMovement.instance.allowInput = false;
        SoundManager.instance.Lose();
        Debug.Log("You lose!");
    }

    public void GoToScene(string sceneName) {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void Restart() {
        GoToScene(CurrentSceneName());
    }

    public void Quit() {
        Application.Quit();
    }

    public void AddTime()
    {
        timeElapsed += 15f;
    }
}

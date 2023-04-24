using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource flopSound;
    public AudioSource spitSound;
    public AudioSource popSound;
    public AudioSource eagleSound;
    public AudioSource musicSource;
    public List<AudioClip> music;
    private int currentMusic = 0;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Flop(float force) {
        GameObject newFlop = Instantiate(flopSound.gameObject);
        AudioSource sound = newFlop.GetComponent<AudioSource>();
        sound.volume = force / 3;
        sound.pitch = Random.Range(0.8f, 1.2f);
        sound.Play();
        Destroy(newFlop, 1f);
    }

    public void Spit() {
        spitSound.pitch = Random.Range(0.8f, 1.2f);
        spitSound.Stop();
        spitSound.Play();
    }

    public void Pop()
    {
        popSound.Play();
    }

    public void Caw()
    {
        eagleSound.Play();
    }

    public void PlayMusic(int index) {
        if (index == currentMusic) return;
        StartCoroutine(FadeToMusic(index));
    }

    IEnumerator FadeToMusic(int index) {
        float startVolume = musicSource.volume;
        for (float i = startVolume; i >= 0; i -= Time.deltaTime / 2f) {
            musicSource.volume = i;
            yield return new WaitForEndOfFrame();
        }
        musicSource.clip = music[index];
        musicSource.Play();
        currentMusic = index;
        for (float i = 0; i <= startVolume; i += Time.deltaTime / 2f) {
            musicSource.volume = i;
            yield return new WaitForEndOfFrame();
        }
    }
}

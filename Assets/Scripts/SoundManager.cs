using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource flopSound;
    public AudioSource spitSound;

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
}

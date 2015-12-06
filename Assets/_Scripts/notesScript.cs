using UnityEngine;
using System.Collections;

public class notesScript : MonoBehaviour {

    private int number;
    private bool isInside,alreadyPlay;
    private AudioSource audio;

    public playPuzzleScript playMusic;


    // Use this for initialization
    void Start () {
        isInside = false;
        alreadyPlay = false;
        audio = GetComponent<AudioSource>();
    }

    void Update() {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        playMusic.pushNote(number);
        playNote();
    }


    void OnTriggerExit2D(Collider2D other)
    {

    }

    public bool isPlaying() {
        return audio.isPlaying;
    }
    
    public void setNumber(int newValue) {
        this.number = newValue;
    }

    public int getNumber() {
        return this.number;
    }

    public void playNote(float delay = 0) {
        audio.PlayDelayed(delay);
    }
}

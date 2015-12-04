using UnityEngine;
using System.Collections;

public class numberTelephonePuzzle : MonoBehaviour {

	// Use this for initialization 
    private AudioSource audio;

    public int number;
    public boxTelephone magicBox;

    // Use this for initialization
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        magicBox.pushNumber(number);
        playNote();
    }


    public void setNumber(int newValue)
    {
        this.number = newValue;
    }

    public int getNumber()
    {
        return this.number;
    }

    public void playNote(float delay = 0)
    {
        audio.PlayDelayed(delay);
    }
}

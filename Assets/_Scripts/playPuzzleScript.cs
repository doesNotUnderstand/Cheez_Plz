using UnityEngine;
using System.Collections;

public class playPuzzleScript : MonoBehaviour {

    //Private 
    private static Stack bufferNotes;
    private int[] correctSequence;
    private int counter;
    private bool unlockKey;
    private float seconds;

    public notesScript blueNote, yellowNote, orangeNote, purpleNote;
    public int sizePassword;
    public GameObject door;
    public AudioSource successAudio, failureAudio;

    // Use this for initialization
    void Start() {
        bufferNotes = new Stack();
        unlockKey = false;
        correctSequence = new int[sizePassword];

        //Select Sequence
        blueNote.setNumber(10);
        yellowNote.setNumber(20);
        orangeNote.setNumber(30);
        purpleNote.setNumber(40);

        //Inicializar
        int i;
        for (i = 0; i < sizePassword; i++) {
            correctSequence[i] = -1;
        }

        bool blue = false, orange = false, purple = false, yellow = false;

        i = 0;
        while (i < sizePassword) {
            int aux = Random.Range(0, sizePassword * sizePassword);
            if (aux % 4 == 0 && !blue)
            {
                correctSequence[i++] = blueNote.getNumber();
                blue = true;
            }
            else if (aux % 4 == 1 && !yellow)
            {
                correctSequence[i++] = yellowNote.getNumber();
                yellow = true;
            }
            else if (aux % 4 == 2 && !orange)
            {
                correctSequence[i++] = orangeNote.getNumber();
                orange = true;
            }
            else if (aux % 4 == 3 && !purple)
            {
                correctSequence[i++] = purpleNote.getNumber();
                purple = true;
            }         
        }
    }

    void Update()
    {

        seconds += Time.deltaTime;
        if (seconds > 4.5)
        {
            cleanBuffer();          
        }
    }

    public void pushNote(int auxValue) {
        seconds = 0;
        bufferNotes.Push(auxValue);
        counter++;

        if (!unlockKey && counter == sizePassword)
        {
            if (isCorrect())
            {
                unlockKey = true;
                Destroy(door);
                successAudio.PlayDelayed(1);
            }
            else {
                //Sound u didnt do it
                failureAudio.PlayDelayed(1);
            }
            cleanBuffer();
            
        }
    }

    private bool isCorrect(){
        int i;
        bool check = true; 
        for (i = (sizePassword-1); i > 0; i--) {
                if ((int)bufferNotes.Pop() != correctSequence[i])
                    check = false;
        }
        return check;       
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        cleanBuffer();
        int i;
        for (i = 0; i < sizePassword; i++)
        {
            if (correctSequence[i] == blueNote.getNumber()) { 
                blueNote.playNote(i);
            }
            else if (correctSequence[i] == yellowNote.getNumber()) {
                yellowNote.playNote(i);
            }              
            else if (correctSequence[i] == orangeNote.getNumber()) {
                orangeNote.playNote(i);
            }
            else if (correctSequence[i] == purpleNote.getNumber()) {
                purpleNote.playNote(i);
            }               
        }     
       
    }

    void cleanBuffer() {
        bufferNotes.Clear();
        counter = 0;
        seconds = 0;
    }
}

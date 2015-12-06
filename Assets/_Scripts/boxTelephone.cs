using UnityEngine;
using System.Collections;
using System;

public class boxTelephone : MonoBehaviour {

    private const int sizePassword = 4;

    private static Stack bufferNumbers;
    private int[,] correctSequence;
    private int counter, selectedPass;   
    private bool unlockKey;
    private float seconds;

    public GameObject door;
    public AudioSource successAudio, failureAudio;
    public Animator anim;

    // Use this for initialization
    void Start()
    {
        bufferNumbers = new Stack();
        unlockKey = false;
        correctSequence = new int[3, sizePassword] { { 1,3,5,7 }, {1,4,5,9}, {0,3,6,7} };

        //Inicializars
        selectedPass = -1;
        while (selectedPass == -1)
        {
            int aux = UnityEngine.Random.Range(0, 3);
            if (aux != 3)
            {
                selectedPass = aux;
                anim.SetInteger("password", selectedPass+1);
            }
        }

        int i;
        for (i = 0; i < sizePassword; i++) {
            //print(correctSequence[selectedPass,i]);
        }
    }

    void Update() {

        seconds += Time.deltaTime;
        if (seconds > 4.5) {
            cleanBuffer();
        }
    }

    public void pushNumber(int number)
    {
        seconds = 0;
        bufferNumbers.Push(number);
        counter++;

        if (!unlockKey && counter == sizePassword)
        {
            if (isCorrect())
            {
                unlockKey = true;
                Destroy(door);
                successAudio.PlayDelayed((float)0.5);
            }
            else
            {
                //Sound u didnt do it
                failureAudio.PlayDelayed((float)0.5);
            }
            cleanBuffer();
        }
    }


    private bool isCorrect()
    {
        int i;
        bool check = true;
        for (i = (sizePassword - 1); i > 0; i--)
        {
            if ((int)bufferNumbers.Pop() != correctSequence[selectedPass,i])
                check = false;
        }
        return check;
    }

    void cleanBuffer()
    {
        bufferNumbers.Clear();
        counter = 0;
        seconds = 0;
    }
}

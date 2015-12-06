using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System;

public class DrawScreen{

    private Texture myTexture;
    private int widthTexture;
    private string name;
    private bool isLeft,hasTimer;
    private double time;

    public DrawScreen(string name, Texture myTex, int width,bool isLeftSideOfScreen,bool hasTimer = false, double time = 3) {
        this.myTexture = myTex;
        this.widthTexture = width;
        this.name = name;
        this.isLeft = isLeftSideOfScreen;
        this.time = time;
        this.hasTimer = hasTimer;
    }

    public Texture getMyTexture(){
        return this.myTexture;
    }
    public int getWidthTexture() {
        return this.widthTexture;
    }
    public string getName() {
        return this.name;
    }
    public bool getIsLeft(){
        return this.isLeft;
    }

    public double getTimeLeft() {
        return this.time;
    }

    public void consumeTime(double addtime) {
        time -= addtime;
    }

    public bool isAlive() {
        if (hasTimer)
            if (time <= 0)
                return false;
        return true;
    }
}

[System.Serializable]
public class Number
{
    public Texture numberOne, numberTwo, numberThree, numberFour, numberFive;
    public Texture numberSix, numberSeven, numberEight, numberNine, numberCero;
    public Texture doublePoint;
}


public class mainCameraScript : MonoBehaviour {

    //Parameters
    public int marginLeft_Clock, marginTop_Clock, widthClock,heightClock;
    public Number numberTimer;

    //Private Variables
    private List<DrawScreen> textureScreen;
    private double levelTime;
    private bool updateTime;
    private int numberItem_Left, numberItem_Right, numberItem_Timer, sumWidth_Right, sumWidth_Left, sumWidth_Timer;
    private string firstDigitTimer, secondDigitTimer, thirdDigitTimer, fourDigitTimer;  
    private Vector3 startPoint;
    public AudioSource source;


    void Start () {
        levelTime = 0; numberItem_Right = 0; numberItem_Left = 0; sumWidth_Right = 0; sumWidth_Left = 0;
        updateTime = true;
        textureScreen = new List<DrawScreen>();
        source = GetComponent<AudioSource>();
    }
	
	void Update () {

        //Timer
        if (updateTime) {
            levelTime += Time.deltaTime;            
            string minutes = Mathf.Floor((float)levelTime / 60).ToString("00");
            firstDigitTimer = minutes.Substring(0, 1);
            secondDigitTimer = minutes.Substring(1, 1);

            string seconds = (levelTime % 60).ToString("00");
            thirdDigitTimer = seconds.Substring(0, 1);
            fourDigitTimer = seconds.Substring(1, 1);

            if (seconds.Equals("60")) {
                thirdDigitTimer = "0";
                minutes = (Int32.Parse(minutes) + 1).ToString("00");
                firstDigitTimer = minutes.Substring(0, 1);
                secondDigitTimer = minutes.Substring(1, 1);
            }
        }
        
    }

    void OnGUI()
    {
        int gapItems = 5, sizeItem = 50;

        //Draw Time 
        numberItem_Timer = 0; sumWidth_Timer = 0;

        drawTimer(firstDigitTimer, numberTimer, sizeItem, gapItems, sizeItem);
        drawTimer(secondDigitTimer, numberTimer, sizeItem, gapItems, sizeItem);
        drawTimer("doublePoint", numberTimer, sizeItem, gapItems, sizeItem);
        drawTimer(thirdDigitTimer, numberTimer, sizeItem, gapItems, sizeItem);
        drawTimer(fourDigitTimer, numberTimer, sizeItem, gapItems, sizeItem);
        

        //Draw items on Screen (More than One) 
        numberItem_Left = 0; numberItem_Right = 0; sumWidth_Right = 0; sumWidth_Left = 0;
        foreach (DrawScreen auxObject in textureScreen) {
            if (auxObject.isAlive())
            {
                if (auxObject.getIsLeft())
                    drawTexture_left(auxObject, gapItems, sizeItem);
                else
                    drawTexture_right(auxObject, gapItems, sizeItem);

                auxObject.consumeTime(Time.deltaTime);
            }
            else
                deleteDrawingOfScreen(auxObject);
         }
    }

    private void drawTimer(string newValue, Number numberTimer, int width, int gapItems, int sizeItem) {

        switch (newValue)
        {
            case "0":
                drawTimerOnScreen(numberTimer.numberCero,width, gapItems, sizeItem);
                break;
            case "1":
                drawTimerOnScreen(numberTimer.numberOne, width, gapItems, sizeItem);
                break;
            case "2":
                drawTimerOnScreen(numberTimer.numberTwo, width, gapItems, sizeItem);
                break;
            case "3":
                drawTimerOnScreen(numberTimer.numberThree, width, gapItems, sizeItem);
                break;
            case "4":
                drawTimerOnScreen(numberTimer.numberFour, width, gapItems, sizeItem);
                break;
            case "5":
                drawTimerOnScreen(numberTimer.numberFive, width, gapItems, sizeItem);
                break;
            case "6":
                drawTimerOnScreen(numberTimer.numberSix, width, gapItems, sizeItem);
                break;
            case "7":
                drawTimerOnScreen(numberTimer.numberSeven, width, gapItems, sizeItem);
                break;
            case "8":
                drawTimerOnScreen(numberTimer.numberEight, width, gapItems, sizeItem);
                break;
            case "9":
                drawTimerOnScreen(numberTimer.numberNine, width, gapItems, sizeItem);
                break;
            case "doublePoint":
                drawTimerOnScreen(numberTimer.doublePoint, width, gapItems, sizeItem);
                break;
            default:
                break;
        }

    }

    private void drawTimerOnScreen(Texture myTexture ,int width, int gapItems, int sizeItem) {
        numberItem_Timer++;
        int posX = sumWidth_Timer + gapItems + gapItems * (numberItem_Timer - 1);

        sumWidth_Timer += width;
        int posY = gapItems ;

        GUI.DrawTexture(new Rect(posX, posY, width, sizeItem), myTexture, ScaleMode.ScaleToFit, true);
    }

    private void drawTexture_right(DrawScreen newObject, int gapItems, int sizeItem){
        numberItem_Right++;
        sumWidth_Right += newObject.getWidthTexture();

        int posX = Screen.width - sumWidth_Right - gapItems - gapItems*(numberItem_Right - 1);
        int posY = Screen.height - gapItems - sizeItem;

        GUI.DrawTexture(new Rect(posX, posY, newObject.getWidthTexture(), sizeItem), newObject.getMyTexture(), ScaleMode.ScaleToFit, true);
    }

    private void drawTexture_left(DrawScreen newObject, int gapItems, int sizeItem)
    {
        numberItem_Left++;
        int posX = sumWidth_Left + gapItems + gapItems * (numberItem_Left - 1);

        sumWidth_Left += newObject.getWidthTexture();
        int posY = Screen.height - gapItems - sizeItem;

        GUI.DrawTexture(new Rect(posX, posY, newObject.getWidthTexture(), sizeItem), newObject.getMyTexture(), ScaleMode.ScaleToFit, true);
    }

    //Setters & Getters
    public void set_levelTime(double newValue) {
        this.levelTime = newValue;
    }

    public int get_levelTime() {
        return (((int)levelTime / 60) * 60) + (((int)levelTime) % 60);        
    }

    public string get_levelTimeSeconds()
    {
        return (levelTime % 60).ToString("00");
    }

    public string get_levelTimeMinutes()
    {
        return Mathf.Floor((float)levelTime / 60).ToString("00");
    }

    public void triggerTimer(bool trigger)
    {
        updateTime = trigger;
    }

    public bool triggerStatus()
    {
        return updateTime;
    }

    public void addDrawingToScreen(DrawScreen newDraw ) {
        textureScreen.Add(newDraw);
    }

    public void deleteDrawingOfScreen(DrawScreen deleteDraw) {
        textureScreen.Remove(deleteDraw);
    }

    public void PlayMusic()
    {
        source.Play();
    }

    public void StopMusic()
    {
        source.Stop();
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawScreen{

    private Texture myTexture;
    private int widthTexture;
    private string name;
    private bool isLeft;

    public DrawScreen(string name, Texture myTex, int width,bool isLeft) {
        this.myTexture = myTex;
        this.widthTexture = width;
        this.name = name;
        this.isLeft = isLeft;
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
}

public class mainCameraScript : MonoBehaviour {

    //Parameters
    public int marginLeft_Clock, marginTop_Clock, widthClock,heightClock;
    public LevelManager levelManager;

    //Private Variables
    private List<DrawScreen> textureScreen;
    private double levelTime;
    private bool updateTime;
    private string elapsedTime;
    private int numberItem_Left, numberItem_Right, sumWidth_Right, sumWidth_Left;
    private Vector3 startPoint;

    void Start () {
        levelTime = 0; numberItem_Right = 0; numberItem_Left = 0; sumWidth_Right = 0; sumWidth_Left = 0;
        updateTime = true;
        textureScreen = new List<DrawScreen>();
    }
	
	void Update () {

        //Timer
        if (updateTime) {
            levelTime += Time.deltaTime;

            string minutes = Mathf.Floor((float)levelTime / 60).ToString("00");
            string seconds = (levelTime % 60).ToString("00");
            elapsedTime = minutes + ":" + seconds + " ";
        }
        
    }

    void OnGUI()
    {
        //Draw Time 
        GUI.Label(new Rect(marginLeft_Clock,marginTop_Clock,widthClock,heightClock), elapsedTime);

        //Draw items on Screen (More than One) 
        numberItem_Left = 0; numberItem_Right = 0; sumWidth_Right = 0; sumWidth_Left = 0;
        int gapItems = 5, sizeItem = 50;
        foreach (DrawScreen auxObject in textureScreen) {
            if (auxObject.getIsLeft())
                drawTexture_left(auxObject,gapItems,sizeItem);
            else 
                drawTexture_right(auxObject, gapItems, sizeItem);       
        }
        
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

    public double get_levelTime() {
        return this.levelTime;
    }

    public void addDrawingToScreen(DrawScreen newDraw ) {
        textureScreen.Add(newDraw);
    }

    public void deleteDrawingOfScreen(DrawScreen deleteDraw) {
        textureScreen.Remove(deleteDraw);
    }

}

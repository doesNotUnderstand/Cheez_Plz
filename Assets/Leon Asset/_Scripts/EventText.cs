using UnityEngine;
using System.Collections;

public class EventText : MonoBehaviour {

    public Transform followObject;
    public Vector2 Offset;
    public UnityEngine.UI.Text text;
    float timer = 0.0f;
    float timerStop = 0.0f;
    bool runTimer = false;

	// Use this for initialization
	void Start () 
    {
        text = GetComponent<UnityEngine.UI.Text>();
        text.text = "";
	}
	
	// The TextBox follows the object position, but not the rotation
	void Update () 
    {
        transform.position = new Vector2(followObject.position.x + Offset.x,
                                         followObject.position.y + Offset.y);
	}

    // Used for changeTimedText()
    void FixedUpdate()
    {
        if(runTimer && timer < timerStop)
        {
            timer += 0.1f;
        }
        else
        {
            runTimer = false;
            text.text = "";
            timer = 0.0f;
        }
    }

    // Change text from other scripts by calling this function
    // If a timed text is in place, it will erase the timed text
    public void changeText(string newText)
    {
        runTimer = false;
        timer = 0.0f;
        text.text = newText;
    }

    // Use this function to set text that disappear after a certain time
    // It will also erase the regular text when called
    public void changeTimedText(string newText, float time)
    {
        text.text = newText;
        timer = 0.0f;
        timerStop = time;
        runTimer = true;
    }
}

using UnityEngine;
using System.Collections;

public class creditScroller : MonoBehaviour 
{
    public RectTransform credits;
    public bool startScript;
    public float scrollSpeed;
    public float textHeightLimit;
    public float initialY;

	// Use this for initialization
	void Start () 
    {
       startScript = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(startScript)
        {
            credits.position = new Vector3(
                                            0,
                                            credits.position.y + scrollSpeed/10 * Time.deltaTime,
                                            500);
            checkTextPosition();
        }
        else
        {
            // Move the credits off the screen when back button is pressed
            credits.position = new Vector3(0, initialY, 0);
        }
	}

    void checkTextPosition()
    {
        if(credits.position.y > textHeightLimit)
        {
            credits.position = new Vector3(-0.7f, initialY, 0);
        }
    }   
}

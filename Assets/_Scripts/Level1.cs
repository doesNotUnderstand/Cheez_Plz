using UnityEngine;
using System.Collections;

public class Level1 : MonoBehaviour {

    public Transform mouse;
    public cheeseBall cheeseBallScript;

	// Use this for initialization
	void Start ()
    {                
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(mouse.GetComponent<playerController>().playerCarryingCheese() && !cheeseBallScript.ballIsMoving())
        {
            cheeseBallScript.setMove();
        }
	}
}

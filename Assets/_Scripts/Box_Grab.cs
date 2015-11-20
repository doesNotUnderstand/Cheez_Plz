using UnityEngine;
using System.Collections;

public class Box_Grab : MonoBehaviour {
	
	// Update is called once per frame
	bool inRange;
	void Start()
	{
		inRange = false;
	}
	void OnCollisionEnter2D(Collision2D c)
	{
		if(c.collider.name == "Mouse")
		{
			inRange = true;
		}
	}
	
	void OnCollisionExit2D(Collision2D c)
	{
		if(c.collider.name == "Mouse")
		{
			inRange = false;
		}
	}
	public bool getBoxRange()
	{
		return inRange;
	}

}

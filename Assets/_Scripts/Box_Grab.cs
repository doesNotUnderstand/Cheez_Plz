using UnityEngine;
using System.Collections;

public class Box_Grab : MonoBehaviour {
	
	// Update is called once per frame
	public GameObject mouse;
	public GameObject parent;
	bool inRange;
	bool carried;
	void Start()
	{
		inRange = false;
		carried = false;
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
	void Update()
	{
		if (inRange && !carried && Input.GetKeyDown(KeyCode.E)) 
		{
			gameObject.transform.parent = mouse.transform;
			carried = !carried;
		}
		else if(carried && Input.GetKeyDown(KeyCode.F))
		{
			gameObject.transform.parent = parent.transform;
			carried = !carried;
		}
	}
}

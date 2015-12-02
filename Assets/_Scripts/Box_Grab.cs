using UnityEngine;
using System.Collections;

public class Box_Grab : MonoBehaviour {
	
	// Update is called once per frame
	public GameObject mouse;
	public GameObject parent;
	public bool attached;
	bool inRange;
	bool carried;
	Vector2 start_vector;
	void Start()
	{
		attached = false;
		inRange = false;
		carried = false;
		start_vector = gameObject.GetComponent<Transform>().position;
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
	public void return_to_start()
	{
		if (!attached) 
		{
			gameObject.GetComponent<Transform> ().position = start_vector;
		}
	}
	void Update()
	{
		if (inRange && !carried && Input.GetKeyDown(KeyCode.E)) 
		{
			gameObject.transform.parent = mouse.transform;
			carried = !carried;
		}
		else if(carried && Input.GetKeyDown(KeyCode.E))
		{
			gameObject.transform.parent = parent.transform;
			carried = !carried;
		}
	}
}

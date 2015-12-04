using UnityEngine;
using System.Collections;

public class doorCollider : MonoBehaviour {
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.name == "Mouse")
		{
			Destroy(this.gameObject);
			foreach (GameObject g in GameObject.FindGameObjectsWithTag("Controller"))
			{
				g.gameObject.GetComponent<pit_timer>().disable();
			}
		}

	}
}

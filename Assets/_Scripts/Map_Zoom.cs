using UnityEngine;
using System.Collections;

public class Map_Zoom : MonoBehaviour {
	float size;
	bool zoomed = false;
	IEnumerator OnTriggerEnter2D(Collider2D c)
	{
		if (c.gameObject.name == "Mouse" && !zoomed) 
		{
			zoomed = true;
			size = GameObject.Find("Minimap").GetComponent<Camera>().orthographicSize;
			GameObject.Find("Minimap").GetComponent<Camera>().orthographicSize = 10;
			gameObject.GetComponent<SpriteRenderer>().enabled = false;
			yield return new WaitForSeconds(3f);
			GameObject.Find("Minimap").GetComponent<Camera>().orthographicSize = size;
			Destroy (gameObject);
		}
	}
}

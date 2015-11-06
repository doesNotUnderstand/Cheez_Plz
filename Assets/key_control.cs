using UnityEngine;
using System.Collections;

public class key_control : MonoBehaviour {

	void Update () {
		if (Input.GetKeyDown("r"))
		{
			Application.LoadLevel(Application.loadedLevel);
		}
	}
}

using UnityEngine;
using System.Collections;

public class DeleteSave : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        PlayerPrefs.DeleteKey("L0Score");
        PlayerPrefs.DeleteKey("L1Score");
        PlayerPrefs.DeleteKey("L2Score");
        Application.Quit();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;

public class testtest : MonoBehaviour {

    public EventText textBox;

	// Use this for initialization
	void Start () 
    {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        textBox.changeTimedText("!", 10.0f);
    }
}

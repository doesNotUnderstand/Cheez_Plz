using UnityEngine;
using System.Collections;

public class cheeseCollider : MonoBehaviour {

    // If the player is in cheese range, he is able to
    // hold the mouse button to carry the cheese
    bool inCheeseRange;

	// Use this for initialization
	void Start () 
    {
        inCheeseRange = false;
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Mouse")
        {
            inCheeseRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.name == "Mouse")
        {
            inCheeseRange = false;
        }
    }

    // Return whether the player in range of the cheese
    public bool getCheeseRange()
    {
        return inCheeseRange;
    }
}

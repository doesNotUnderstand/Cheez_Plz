using UnityEngine;
using System.Collections;

public class cheeseCollider : MonoBehaviour {

    // If the player is in cheese range, he is able to
    // hold the mouse button to carry the cheese
    public bool resetsOnSpawn;

    public Vector2 initPosition;
    bool inCheeseRange;

	// Use this for initialization
	void Start () 
    {
        initPosition = GetComponent<Transform>().position;
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

    // Return the cheese to initial position
    public void resetPosition()
    {
        this.GetComponent<Transform>().position = initPosition;
    }
}

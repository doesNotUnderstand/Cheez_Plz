using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

    public Transform player; // Attach the player(mouse) here
    float distance = 1;
	
	// This class allows the camera to follow the player without
    // rotating when the character does
	void Update () 
    {
            transform.position = new Vector3(player.position.x,
                                             player.position.y,
                                             player.position.z - distance);
	}
}

using UnityEngine;
using System.Collections;

public class fillable_pit : MonoBehaviour {
	bool filled;
	public playerController playerScript;
    AudioSource audio;
	// Use this for initialization
	void Start () {
		filled = false;
        audio = GetComponent<AudioSource>();
	}
	
	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.name == "Box") {
			c.transform.parent = null;
			c.transform.position = gameObject.transform.position;
			gameObject.GetComponent<BoxCollider2D> ().enabled = false;
			c.gameObject.GetComponent<BoxCollider2D> ().enabled = false;
			filled = true;
            if (!audio.isPlaying)
            {
                audio.Play();
            }
            playerScript.setCarrying(false);
		} 
		else if(c.name == "Mouse")
		{
			playerScript.anim.SetBool("MouseFell", true);
			playerScript.allowMovement(false);
			playerScript.setPlayerDied(true);
		}
	}
	public bool getFilled()
	{
		return filled;
	}
}

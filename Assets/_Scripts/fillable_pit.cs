using UnityEngine;
using System.Collections;

public class fillable_pit : MonoBehaviour {
	bool filled;
    bool isLevel1 = false;
    bool isLevel2 = false;
	public playerController playerScript;
    AudioSource audio;
	// Use this for initialization
	void Start () {
		filled = false;
        audio = GetComponent<AudioSource>();
        if (Application.loadedLevel == 2)
        {
            isLevel1 = true;
        }
        if (Application.loadedLevel == 3)
        {
            isLevel2 = true;
        }
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
            if (isLevel1)
            {
                GameObject.Find("Cheese Ball").GetComponent<cheeseBall>().resetPosition();
            }
            if(isLevel2)
            {
                GameObject.Find("Controller_Trigger").GetComponent<pit_activator>().reset_controller();

            }
        }
	}
	public bool getFilled()
	{
		return filled;
	}
}

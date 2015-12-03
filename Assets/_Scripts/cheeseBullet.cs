using UnityEngine;
using System.Collections;

public class cheeseBullet : MonoBehaviour {

    public playerController playerScript;

	// Use this for initialization
	void Start ()
    {        
        playerScript = GetComponent<Transform>().parent.gameObject.GetComponent<turret>().playerScript;
    }

    void OnTriggerEnter2D(Collider2D other)
    { 
        if (other.name == "Mouse")
        {
            playerScript.anim.SetBool("MouseFell", true);
            playerScript.allowMovement(false);
            playerScript.setPlayerDied(true);
            Destroy(this.gameObject);
        }
        else if(other.tag == "DestroyBullet")
        {
            Destroy(this.gameObject);
        }
    }
}

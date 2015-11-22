using UnityEngine;
using System.Collections;

public class cheeseBall : MonoBehaviour {

    public Transform target;
    public playerController playerScript;
    public float speed;

    bool moving;
    Transform cheeseBallTransform;
    Vector2 startingPosition;

	// Use this for initialization
	void Start ()
    {
        cheeseBallTransform = GetComponent<Transform>();
        startingPosition = cheeseBallTransform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {        
        MoveToTarget();        
	}

    public void setMove()
    {
        moving = true;
    }

    public bool ballIsMoving()
    {
        return moving;
    }

    public void resetPosition()
    {
        moving = false;
        cheeseBallTransform.position = startingPosition;
    }

    void MoveToTarget()
    {
        if(moving)
        {
            cheeseBallTransform.position = Vector2.MoveTowards(cheeseBallTransform.position, target.position, speed * Time.deltaTime);         
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Mouse")
        {
            playerScript.anim.SetBool("MouseFell", true);
            playerScript.allowMovement(false);
            playerScript.setPlayerDied(true);
            resetPosition();
        }
    }
}

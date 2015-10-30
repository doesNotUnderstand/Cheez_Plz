using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public bool levelStart = false; // Change to private later
    public Vector2 startPoint;
    Transform player;
    playerController playerScript;
    bool moveToSpawn;
    bool timerOn;
    float deathTimer;

	// Use this for initialization
	void Start () 
    {
        player = GetComponent<Transform>();
        playerScript = GetComponent<playerController>();
        moveToSpawn = true;
        levelStart = true; //Disable later - Enabled for testing
        deathTimer = 0.0f;
        timerOn = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(levelStart)
        {
            if(moveToSpawn)
            {
                player.position = new Vector3(startPoint.x, startPoint.y, 0);
                playerScript.anim.SetBool("MouseFell", false);
                moveToSpawn = false;
                playerScript.allowMovement(true);
            }

            if(playerScript.getPlayerDied())
            {
                timerOn = true;
                playerScript.setPlayerDied(false);
            }

            DeathTimer();
        }
	}

    void DeathTimer()
    {
        if(timerOn)
        {
            deathTimer += Time.deltaTime;
        }

        if(deathTimer > 2.0f)
        {
            timerOn = false;
            deathTimer = 0.0f;
            moveToSpawn = true;
        }
    }
}

using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public bool levelStart = false; // Change to private later
    public Vector2 startPoint;
    public mainCameraScript cameraScript;
    public Texture textureKey;
    Transform player;
    playerController playerScript;
    bool moveToSpawn;
    bool timerOn;
	float speed;
    float deathTimer;

    //Drawing
    DrawScreen keyDraw;

	// Use this for initialization
	void Start () 
    {
		speed = GameObject.Find ("Mouse").GetComponent<playerController> ().get_speed ();
        player = GetComponent<Transform>();
        playerScript = GetComponent<playerController>();
        moveToSpawn = true;
        levelStart = true; //Disable later - Enabled for testing
        deathTimer = 0.0f;
        timerOn = false;
        keyDraw = null;
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(levelStart)
        {
            if(moveToSpawn)
            {
				GameObject.Find("Mouse").GetComponent<playerController>().set_speed(speed);
                player.position = new Vector3(startPoint.x, startPoint.y, 0);                               
			
				foreach(GameObject g in GameObject.FindGameObjectsWithTag("Trap"))
				{
					g.SetActive(true);
					g.GetComponent<SpriteRenderer>().enabled = true;
				}
				
                playerScript.anim.SetBool("MouseFell", false);                
                moveToSpawn = false;
                playerScript.allowMovement(true);
            }

            if(playerScript.getPlayerDied())
            {
                timerOn = true;
                playerScript.setPlayerDied(false);
            }
            drawKey();
            DeathTimer();
        }
	}

    public void startLevel()
    {
        levelStart = true;
    }

    void drawKey()
    {        
        if (playerScript.getKeyState() && keyDraw == null)
        {
            keyDraw = new DrawScreen("key1", textureKey, 30, false);
            cameraScript.addDrawingToScreen(keyDraw);
        }
        else if (!playerScript.getKeyState() && keyDraw != null)
        {
            cameraScript.deleteDrawingOfScreen(keyDraw);
            keyDraw = null;
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
            player.eulerAngles = new Vector3(0, 0, 0);
            player.localScale = new Vector3(0.3f, 0.3f, 1);
            timerOn = false;
            deathTimer = 0.0f;
            moveToSpawn = true;
        }
    }
}

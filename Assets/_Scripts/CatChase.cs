using UnityEngine;
using System.Collections;

public class CatChase : MonoBehaviour {

    public Transform playerTransform;
    public playerController playerScript;
    public Vector3 centerPoint; // The point from which the distance is calculated
    public float catSpeed; // The cat's speed
    public float range = 0.0f; // The set distance to chase the mouse
    public float catchRange = 0.0f; // The range where the cat catches the mouse
    public EventText textBox; // In case the cat needs to display a textBox
    Transform catTransform;
    bool catCanMove;
    bool alreadyChasing = false; // Used to prevent the cat from stopping pursuit when player crouches

    public GameObject bigCollider;
    private bool bigColliderIsActive = false;

    // Use this for initialization
    void Start () 
    {
        catTransform = GetComponent<Transform>();
        catCanMove = true;
        bigCollider.SetActive(bigColliderIsActive);
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (inCatchRange())
        {
			playerScript.setPlayerDied(true);
			catCanMove = false;
            playerScript.anim.SetBool("MouseFell", true);
            playerScript.allowMovement(false);

            playerScript.setPlayerDied(true);
			foreach(GameObject g in GameObject.FindGameObjectsWithTag("Ghost"))
			{
				g.GetComponent<GhostCat>().destroy_cat();
			}

            StartCoroutine(waitThenReturn());
        }

        if (catCanMove)
        {
            if (inRange() && (!playerScript.playerIsCrouching() || alreadyChasing))
            {
                textBox.changeTimedText("!", 10.0f);
                chasePlayer();
            }
            else
            {
                alreadyChasing = false;
                moveBackToCenter();
            }
        }

        //This SHould be Change
        if (Input.GetKeyDown(KeyCode.B)) {
            if (bigColliderIsActive)
            {
                bigColliderIsActive = !bigColliderIsActive;
                bigCollider.SetActive(bigColliderIsActive);
            }
            else {
                bigColliderIsActive = !bigColliderIsActive;
                bigCollider.SetActive(bigColliderIsActive);
            }
           
        }


	}

    // Return whether the mouse is in cat's range
    bool inRange()
    {
        return Vector3.Distance(playerTransform.position, centerPoint) <= range;
    }

    IEnumerator waitThenReturn()
    {
        yield return new WaitForSeconds(3);
        catCanMove = true;
    }

    bool inCatchRange()
    {
        return Vector3.Distance(playerTransform.position, catTransform.position) <= catchRange;
    }

	public bool ghostRange()
	{
		return Vector3.Distance(playerTransform.position, catTransform.position) <= catchRange;
	}

    void chasePlayer()
    {
        alreadyChasing = true;
        if(playerTransform.position.x < centerPoint.x)
        {
            catTransform.position = new Vector3(catTransform.position.x - catSpeed * Time.deltaTime,
                                                catTransform.position.y, catTransform.position.z);
        }
        else if(playerTransform.position.x > centerPoint.x)
        {
            catTransform.position = new Vector3(catTransform.position.x + catSpeed * Time.deltaTime,
                                                catTransform.position.y, 0);
        }

        if(playerTransform.position.y < centerPoint.y)
        {
            catTransform.position = new Vector3(catTransform.position.x,
                                                catTransform.position.y - catSpeed * Time.deltaTime, 0);
        }
        else if (playerTransform.position.y > centerPoint.y)
        {
            catTransform.position = new Vector3(catTransform.position.x,
                                                catTransform.position.y + catSpeed * Time.deltaTime, 0);
        }  
    }

    void moveBackToCenter()
    {
        if (!atCenter())
        {
            if (catTransform.position.x > centerPoint.x)
            {
                catTransform.position = new Vector3(catTransform.position.x - catSpeed * Time.deltaTime,
                                                    catTransform.position.y, catTransform.position.z);
            }
            else if (catTransform.position.x < centerPoint.x)
            {
                catTransform.position = new Vector3(catTransform.position.x + catSpeed * Time.deltaTime,
                                                    catTransform.position.y, 0);
            }

            if (catTransform.position.y > centerPoint.y)
            {
                catTransform.position = new Vector3(catTransform.position.x,
                                                    catTransform.position.y - catSpeed * Time.deltaTime, 0);
            }
            else if (catTransform.position.y < centerPoint.y)
            {
                catTransform.position = new Vector3(catTransform.position.x,
                                                    catTransform.position.y + catSpeed * Time.deltaTime, 0);
            }
        }
    }

    bool atCenter()
    {
        return (catTransform.position.x <= centerPoint.x + 0.1f && catTransform.position.x >= centerPoint.x - 0.1f) &&
               (catTransform.position.y <= centerPoint.y + 0.1f && catTransform.position.y >= centerPoint.y - 0.1f);
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(PolyNavAgent))]
public class CatChase_ : MonoBehaviour {

    private PolyNavAgent _agent;

    public PolyNavAgent agent
    {
        get
        {
            if (!_agent)
                _agent = GetComponent<PolyNavAgent>();
            return _agent;
        }
    }

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

    // These are used when the cats only "patrol" (i.e. they move from waypoint to waypoint).
    public bool patrol;
    public Vector2 waypoint = new Vector2();
    Vector2 originWaypoint = new Vector2();


	// Use this for initialization
	void Start () 
    {
        catTransform = GetComponent<Transform>();
        catCanMove = true;

        if (patrol)
        {
            originWaypoint = new Vector2(catTransform.position.x,
                                         catTransform.position.y);
            agent.SetDestination(waypoint);
        }
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
        agent.SetDestination(new Vector2(playerTransform.position.x,
                                         playerTransform.position.y));
    }

    void moveBackToCenter()
    {
        if (!atCenter())
        {
            agent.SetDestination(new Vector2(centerPoint.x,
                                             centerPoint.y));
        }
    }

    bool atCenter()
    {
        return (catTransform.position.x <= centerPoint.x + 0.1f && catTransform.position.x >= centerPoint.x - 0.1f) &&
               (catTransform.position.y <= centerPoint.y + 0.1f && catTransform.position.y >= centerPoint.y - 0.1f);
    }

    void SetDestination()
    {
        if (catTransform.position.y == originWaypoint.y - 0.1f)
        {
            agent.SetDestination(waypoint);
        }
        else {
            agent.SetDestination(originWaypoint);
        }
    }

    void OnEnable()
    {
        agent.OnDestinationReached += SetDestination;
        agent.OnDestinationInvalid += SetDestination;
    }

    void OnDisable()
    {
        agent.OnDestinationInvalid -= SetDestination;
        agent.OnDestinationReached -= SetDestination;
    }
}

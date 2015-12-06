#define ENABLE_ANIMATION

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(PolyNavAgent))]
public class CatChase : MonoBehaviour {

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

    private const bool DEBUG = false;

    public Transform playerTransform;
    public playerController playerScript;
    public Vector3 centerPoint; // The point from which the distance is calculated
    public float catSpeed; // The cat's speed
    public float range = 0.0f; // The set distance to chase the mouse
    public float catchRange = 0.0f; // The range where the cat catches the mouse
    public EventText textBox; // In case the cat needs to display a textBox
    Transform catTransform;
    bool catCanMove = true;
    bool alreadyChasing = false; // Used to prevent the cat from stopping pursuit when player crouches
    Vector2 lastPosition;

    public Animator anim;

    // These are used when the cats only "patrol" (i.e. they move from waypoint to waypoint).
    public bool patrol;
    public Vector2 waypoint = new Vector2();
    Vector2 originWaypoint = new Vector2();

    System.Random rand = new System.Random();

    enum Direction { Down, Left, Up, Right };

    // Use this for initialization
    void Awake()
    {
        catTransform = GetComponent<Transform>();
        anim = GetComponent<Animator>();

        if (DEBUG)
            Debug.Log("Cat's current position is: " + catTransform.position.x + ", " + catTransform.position.y);
    }

    void Start()
    {
        lastPosition = new Vector2(catTransform.position.x,
                                   catTransform.position.y);
        originWaypoint = new Vector2(catTransform.position.x,
                                     catTransform.position.y);       

        if (patrol)
        {            
            agent.SetDestination(waypoint);
        }
    }

    void FixedUpdate()
    {
        if (inKillRange()) {
            playerDeath();
        }
        else if (catCanMove) {
            if (inVisibleRange() &&
                (!playerScript.playerIsCrouching() || !alreadyChasing))
            {
                Debug.Log("Chasing player");
                chasePlayer();
            }
            else if (!patrol)
            {
                agent.SetDestination(originWaypoint);
            }
        }

        Vector2 currentDirection = new Vector2(catTransform.position.x,
                                               catTransform.position.y);
        handlePlayerAnimations(direction(currentDirection));
        lastPosition = currentDirection;

        if (DEBUG)
            Debug.Log("Cat is Sleeping? " + anim.GetBool("Sleeping"));
    }

    Vector2 direction(Vector2 currentPosition)
    {
        Vector2 heading, direction;
        if (currentPosition != lastPosition)
        {
            heading = currentPosition - lastPosition;
            direction = heading / heading.magnitude;
        }
        else
        {
            direction = new Vector2(0f, 0f);
        }
        return direction;
    }

    void chasePlayer()
    {
        alreadyChasing = true;
        agent.SetDestination(new Vector2(playerTransform.position.x,
                                         playerTransform.position.y));
    }

    IEnumerator waitThenReturn()
    {
        yield return new WaitForSeconds(3);        
        catCanMove = true;        
    }

    void playerDeath()
    {
        playerScript.setPlayerDied(true);
        alreadyChasing = catCanMove = false;
        playerScript.anim.SetBool("MouseFell", true);
        playerScript.allowMovement(false);

        foreach (GameObject gObject in GameObject.FindGameObjectsWithTag("Ghost"))
        {
            gObject.GetComponent<GhostCat>().destroy_cat();
        }

        if (!patrol)
        {
            agent.SetDestination(originWaypoint);
        }
        else
        {
            agent.SetDestination(nearestWaypoint(new Vector2(catTransform.position.x,
                                                             catTransform.position.y)));
        }
        StartCoroutine(waitThenReturn());
    }

    Vector2 nearestWaypoint(Vector2 currentPosition)
    {
        Vector2 nearestWaypoint;

        if (Vector2.Distance(currentPosition, originWaypoint) <=
            Vector2.Distance(currentPosition, waypoint))
        {
            nearestWaypoint = originWaypoint;
        }
        else
        {
            nearestWaypoint = waypoint;
        }

        return nearestWaypoint;
    }

    bool inVisibleRange()
    {
        return Vector3.Distance(playerTransform.position,
                                catTransform.position) <= range;
    }

    bool inKillRange()
    {
        bool inRange = Vector3.Distance(playerTransform.position,
                                        catTransform.position) <= catchRange;

        if (DEBUG)
            Debug.Log("Is Mouse in Range? " + inRange);

        return inRange;
    }

    void OnEnable()
    {
        agent.OnDestinationReached += SetWaypoint;
        agent.OnDestinationInvalid += SetWaypoint;
    }

    void OnDisable()
    {
        agent.OnDestinationReached -= SetWaypoint;
        agent.OnDestinationInvalid -= SetWaypoint;
    }

    void SetWaypoint()
    {        
        if (patrol)
        {
            if (rand.Next(0, 2) == 1)
                agent.SetDestination(originWaypoint);
            else
                agent.SetDestination(waypoint);
        }
        else
        {
            agent.Stop();            
        }
    }
     
    void handlePlayerAnimations(Vector2 direction)
    {      
        // Calculate the primary direction.
        var x = direction.x;
        var y = direction.y;

        // Debug.Log("Cat Direction: (" + x + ", " + y + ")");

        if (x == 0 && y == 0)
        {
            anim.SetBool("Sleeping", true);            
            return;
        }
        else
        {
            anim.SetBool("Sleeping", false);
            anim.SetInteger("Direction", (int)Direction.Left);
        }
        
        if (Mathf.Abs(x) < Mathf.Abs(y))
        {
            if (y < 0)
            {
                anim.SetInteger("Direction", (int)Direction.Down);
                Debug.Log("Moving Down");
            }
            else
            {
                anim.SetInteger("Direction", (int)Direction.Up);
                Debug.Log("Moving Up");
            }
        }
        else
        {
            if (x < 0)
            {
                anim.SetInteger("Direction", (int)Direction.Left);                
                Debug.Log("Moving Left");
            }
            else
            {
                anim.SetInteger("Direction", (int)Direction.Right);                
                Debug.Log("Moving Right");
            }
        }        
    }
}
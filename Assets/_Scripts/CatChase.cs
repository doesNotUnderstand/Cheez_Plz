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
    private const bool ANIMATION = true;

    // External Controllers
    public Transform playerTransform;
    public playerController playerScript;
    public Animator anim;
    private GameObject bigCollider;
    private GameObject smallCollider;
    private PolyNavAgent polyNav;

    // public Vector3 centerPoint; // The point from which the distance is calculated
    public float catSpeed; // The cat's speed
    public float range = 0.0f; // The set distance to chase the mouse    
    public EventText textBox; // In case the cat needs to display a textBox
    Transform catTransform;
    bool catCanMove = true;
    bool alreadyChasing = false; // Used to prevent the cat from stopping pursuit when player crouches
    Vector2 lastPosition;    

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

        bigCollider = catTransform.FindChild("BigCollider").gameObject;
        smallCollider = catTransform.FindChild("SmallCollider").gameObject;

        polyNav = GetComponent<PolyNavAgent>();
        polyNav.maxSpeed = catSpeed;

        changeColliders(anim.GetBool("Sleeping"));

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
        if (catCanMove) {
            if (inVisibleRange() &&
                !playerScript.playerIsCrouching() || !alreadyChasing)
            {
                if (!DEBUG)
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

        changeColliders(anim.GetBool("Sleeping"));
    }

    void changeColliders(bool CatIsSleeping)
    {
        if (CatIsSleeping)
        {
            smallCollider.SetActive(true);
            bigCollider.SetActive(false);
        }
        else
        {
            if (DEBUG)
                Debug.Log("Chasing Collider: ENABLED");
            smallCollider.SetActive(false);
            bigCollider.SetActive(true);
        }
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
        Debug.Log("Mouse/Player killed.");
        playerScript.setPlayerDied(true);
        alreadyChasing = catCanMove = false;
        playerScript.anim.SetBool("MouseFell", true);
        playerScript.allowMovement(false);        

        foreach (GameObject gObject in GameObject.FindGameObjectsWithTag("Ghost"))
        {
            gObject.GetComponent<GhostCat>().destroyGhostCat();
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

    bool inVisibleRange()
    {
        return Vector3.Distance(playerTransform.position,
                                catTransform.position) <= range;
    }

    #region == Collision Delegates ==
    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.collider.name.Equals("Mouse"))
            playerDeath();
    }
    #endregion

    #region == PolyNav2D Support Methods ==
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
    #endregion

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
                if (DEBUG)
                    Debug.Log("Moving Down");
            }
            else
            {
                anim.SetInteger("Direction", (int)Direction.Up);
                if (DEBUG)
                    Debug.Log("Moving Up");
            }
        }
        else
        {
            if (x < 0)
            {
                anim.SetInteger("Direction", (int)Direction.Left);  
                if (DEBUG)              
                    Debug.Log("Moving Left");
            }
            else
            {
                anim.SetInteger("Direction", (int)Direction.Right);    
                if (DEBUG)            
                    Debug.Log("Moving Right");
            }
        }        
    }
}
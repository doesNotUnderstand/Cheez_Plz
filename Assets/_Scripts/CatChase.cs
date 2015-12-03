using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    // These are used when the cats only "patrol" (i.e. they move from waypoint to waypoint).
    public bool patrol;
    public Vector2 waypoint = new Vector2();
    Vector2 originWaypoint = new Vector2();

    System.Random rand = new System.Random();

	// Use this for initialization
    void Awake()
    {
        catTransform = GetComponent<Transform>();
        originWaypoint = new Vector2(catTransform.position.x,
                                     catTransform.position.y);
        agent.SetDestination(waypoint);
        Debug.Log("Cat's current position is: " + catTransform.position.x + ", " + catTransform.position.y);
    }

    void Update()
    {
        if (inCatchRange()) {
            playerDeath();
        } else if (catCanMove) {
            chasePlayer();
        }
    }

    void chasePlayer()
    {
        alreadyChasing = true;
        agent.SetDestination(new Vector2(playerTransform.position.x,
                                         playerTransform.position.y));
    }

    void playerDeath()
    {
        playerScript.setPlayerDied(true);
        catCanMove = false;
        playerScript.anim.SetBool("MouseFell", true);
        playerScript.allowMovement(false);

        foreach (GameObject gObject in GameObject.FindGameObjectsWithTag("Ghost"))
        {
            gObject.GetComponent<GhostCat>().destroy_cat();
        }
    }

    bool inRange()
    {
        return Vector3.Distance(playerTransform.position,
                                catTransform.position) <= range;
    }

    bool inCatchRange()
    {
        bool inRange = Vector3.Distance(playerTransform.position,
                                        catTransform.position) <= catchRange;
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
        if (rand.Next(0, 2) == 1)
            agent.SetDestination(originWaypoint);
        else
            agent.SetDestination(waypoint);
    }
}

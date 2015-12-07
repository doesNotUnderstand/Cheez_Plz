using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GhostCat : MonoBehaviour
{
    GameObject ghostCat, player;
    public List<Vector2> spawnPoints;
    public float lifespan = 0;
    private bool ghostCatHasSpawned = false;

    void OnTriggerEnter2D (Collider2D collider)
    {
        if (collider.name.Equals("Mouse") && !ghostCatHasSpawned)
        {
            player = collider.gameObject;
            Debug.Log("Spawning ghost cat.");

            initGhostCat();        
        }
    }

    void initGhostCat()
    {
        ghostCatHasSpawned = true;

        ghostCat = Instantiate(Resources.Load("Cat", typeof(GameObject)),
                               getNearestSpawnPoint(player.transform.position),
                               Quaternion.identity) as GameObject;
        ghostCat.name = "Ghost";
        ghostCat.GetComponent<CatChase>().playerScript = player.GetComponent<playerController>();
        ghostCat.GetComponent<CatChase>().playerTransform = player.transform;
        ghostCat.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.45f);
    }

    void Update()
    {
        if (ghostCatHasSpawned)
        {
            lifespan += 2 * Time.deltaTime;
        }

        if (player != null && player.GetComponent<playerController>().getPlayerDied() && ghostCatHasSpawned)
            destroyGhostCat();
        else if (lifespan > 10)
            destroyGhostCat();

    }

    public bool destroyGhostCat()
    {
        Destroy(ghostCat);
        ghostCatHasSpawned = false;
        lifespan = 0;
        return true;
    }

    Vector3 getNearestSpawnPoint(Vector2 playerPosition)
    {
        float distance = 10000;
        Vector2 spawnHere = new Vector2();

        foreach (Vector2 spawnPoint in spawnPoints)
        {
            if (Vector2.Distance(playerPosition, spawnPoint) < distance)
                spawnHere = spawnPoint;
        }

        return new Vector3(spawnHere.x, spawnHere.y);
    }
}
	
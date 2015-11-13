using UnityEngine;
using System.Collections;

public class GhostCat: MonoBehaviour {
	bool ghost_spawned = false;
	GameObject g = null;
	GameObject target = null;
	playerController playerScript;
	public float x_pos;
	public float y_pos;
	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.name == "Mouse" && !ghost_spawned)
		{ 
			target = c.gameObject;
			//spawn a cat
			//give it a vector location
			g = Instantiate(Resources.Load("Prefabs/Cat", typeof(GameObject)), 
			                           new Vector3(x_pos, y_pos), Quaternion.identity) as GameObject;

			//ai specific section below
			//may change when we get final ai
			g.GetComponent<CatChase>().playerScript = GameObject.Find ("Mouse").GetComponent<playerController>();
			g.GetComponent<CatChase>().playerTransform = GameObject.Find("Mouse").transform;
			g.GetComponent<CatChase>().centerPoint.x = x_pos;
			g.GetComponent<CatChase>().centerPoint.y = y_pos;
			g.GetComponent<CatChase>().textBox = GameObject.Find("Text").GetComponent<EventText>();
			g.GetComponent<CatChase>().range = 120000f;
			g.GetComponent<CatChase>().catSpeed = 2.15f;
			//end of ai specific section

			//make the ghost cat slightly transparent
			g.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);

			//limit the amount of ghosts this object can spawn to 1
			ghost_spawned = true;
		}
	}
	/* Destroys the ghost that this object spawned
	 * allows for a new ghost to spawned if triggered again
	 */
	public void destroy_cat()
	{
		Destroy(g);
		ghost_spawned = false;
	}
	void Update () 
	{
		//If mouse is not null (mostly here to avoid the console going crazy with errors because target
		//is only given a value when this object is triggered) and it is caught, destroy
		//the ghost. Or if the mouse gets too far away, destroy the ghost.
		if (target != null && target.GetComponent<playerController>().getPlayerDied() && ghost_spawned) 
		{
			destroy_cat();
		}
		else if (ghost_spawned && Vector2.Distance(g.transform.position, target.transform.position) > 4)
		{
			destroy_cat();
		}
	}
}

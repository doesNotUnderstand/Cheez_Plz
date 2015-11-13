using UnityEngine;
using System.Collections;

public class StunDetector : MonoBehaviour {

	IEnumerator OnTriggerEnter2D(Collider2D c)
	{
		float o_speed = c.gameObject.GetComponent<playerController> ().get_speed ();
		c.gameObject.GetComponent<playerController> ().set_speed (0);
		yield return new WaitForSeconds (2f);
		c.gameObject.GetComponent<playerController> ().set_speed (o_speed);
		Destroy (gameObject);
	}
}

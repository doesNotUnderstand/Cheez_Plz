using UnityEngine;
using System.Collections;

public class haste : MonoBehaviour {
	GameObject target;
	bool speed_up = false;
	float o_speed;
    AudioSource audio;

	IEnumerator OnTriggerEnter2D(Collider2D c)
	{
		if (c.name == "Mouse" && !speed_up) 
		{

            audio = GetComponent<AudioSource>();
            audio.Play();
            target = c.gameObject;
			speed_up = true;
			o_speed = target.GetComponent<playerController> ().get_speed ();
			target.GetComponent<playerController> ().set_speed (o_speed * 1.5f);
			gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			yield return new WaitForSeconds (5f);
			target.GetComponent<playerController> ().set_speed (o_speed);
			gameObject.SetActive (false);
		}
	}
}

﻿using UnityEngine;
using System.Collections;

public class StunDetector : MonoBehaviour {

    public float stunTime = 2f;
    public mainCameraScript cameraScript;
    public Texture stunTexture;
   

	IEnumerator OnTriggerEnter2D(Collider2D c)
	{
		float o_speed = (float) c.gameObject.GetComponent<playerController>().get_speed();
		c.gameObject.GetComponent<playerController>().set_speed (0);
		yield return new WaitForSeconds (stunTime);
		c.gameObject.GetComponent<playerController> ().set_speed (o_speed);
		Destroy (gameObject);

        cameraScript.addDrawingToScreen(new DrawScreen("",stunTexture,30,true,true,0.5));
	}
}

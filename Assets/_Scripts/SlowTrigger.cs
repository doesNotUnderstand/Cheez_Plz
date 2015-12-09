using UnityEngine;
using System.Collections;

public class SlowTrigger : MonoBehaviour {
	GameObject target;
	float o_speed;
	bool sticking = false;
    AudioSource audio;

    public mainCameraScript mainCamera;
    public Texture bubbleTexture;
    private DrawScreen drawBubble;

	IEnumerator speed_up()
	{
		target.GetComponent<playerController> ().set_speed (3.5f * 1.75f);
		yield return new WaitForSeconds(.5f);
		target.GetComponent<playerController> ().set_speed (o_speed);
    }
	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.gameObject.name == "Mouse") 
		{
            audio = GetComponent<AudioSource>();
            if (!audio.isPlaying)
            {
                audio.Play();
            }
			target = c.gameObject;
			sticking = true;
			o_speed = target.GetComponent<playerController>().get_speed();
			target.GetComponent<playerController>().set_speed(1.2f);

            drawBubble = new DrawScreen("", bubbleTexture, 30, true);
            mainCamera.addDrawingToScreen(drawBubble);
        }
	}

    void OnTriggerExit2D(Collider2D c) {

        if (c.gameObject.name == "Mouse")
        {
            mainCamera.deleteDrawingOfScreen(drawBubble);
            drawBubble = null;
            target.GetComponent<playerController>().set_speed(o_speed);
        }
    }

	void Update () 
	{
		if (sticking) 
		{
			float distance = Vector2.Distance(gameObject.transform.position, target.transform.position);
			if(distance > 1.5)
			{
				sticking = false;
				StartCoroutine(speed_up());
			}
		}
	}
}

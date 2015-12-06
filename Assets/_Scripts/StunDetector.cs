using UnityEngine;
using System.Collections;

public class StunDetector : MonoBehaviour {

    AudioSource audio;
    bool isDestroy = false;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (isDestroy && !audio.isPlaying)
        {
            Destroy(gameObject);
        }
    }
	IEnumerator OnTriggerEnter2D(Collider2D c)
	{
        if (c.gameObject.name == "Mouse")
        {
            if (!audio.isPlaying)
            {
                audio.Play();
            }
            float o_speed = c.gameObject.GetComponent<playerController>().get_speed();
            c.gameObject.GetComponent<playerController>().set_speed(0);
            yield return new WaitForSeconds(2f);
            c.gameObject.GetComponent<playerController>().set_speed(o_speed);
            isDestroy = true;
        }
	}
}

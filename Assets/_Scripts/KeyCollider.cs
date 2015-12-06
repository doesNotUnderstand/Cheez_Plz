using UnityEngine;
using System.Collections;

public class KeyCollider : MonoBehaviour {

    public playerController playerScript;
    public AudioSource audio;
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
    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.name == "Mouse")
        {
            // Mouse got the key
            playerScript.playerKeyState(true);
            isDestroy = true;
            if (!audio.isPlaying)
            {
                audio.Play();
            }
            GetComponent<SpriteRenderer>().sprite = null;
        }
    }
}

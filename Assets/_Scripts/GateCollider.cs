using UnityEngine;
using System.Collections;

public class GateCollider : MonoBehaviour {

    public playerController playerScript;
    public GameObject gateObject;
    public AudioSource locked, open;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Mouse" && playerScript.playerHasGateKey())
        {            
            // Open the Gate
            if (!locked.isPlaying && gateObject)
            {
                open.Play();
            }
            playerScript.playerKeyState(false);
            Destroy(gateObject);
        }
        else
        {
            // Play locked gate sound here, possibly gate rattle animation
            if (!open.isPlaying && gateObject)
            {
                locked.Play();
            }
        }
        
    }
}

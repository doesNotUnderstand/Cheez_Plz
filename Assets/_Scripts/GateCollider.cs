using UnityEngine;
using System.Collections;

public class GateCollider : MonoBehaviour {

    public playerController playerScript;
    public GameObject gateObject;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Mouse" && playerScript.playerHasGateKey())
        {            
            // Open the Gate
            playerScript.playerKeyState(false);
            Destroy(gateObject);
        }
        else
        {
            // Play locked gate sound here, possibly gate rattle animation
            
        }
        
    }
}

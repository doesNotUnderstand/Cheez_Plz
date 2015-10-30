using UnityEngine;
using System.Collections;

public class KeyCollider : MonoBehaviour {

    public playerController playerScript;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Mouse")
        {
            // Mouse got the key
            playerScript.playerKeyState(true);
            Destroy(gameObject);
        }
    }
}

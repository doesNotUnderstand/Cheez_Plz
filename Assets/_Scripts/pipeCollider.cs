using UnityEngine;
using System.Collections;

public class pipeCollider : MonoBehaviour {

    public Collider2D pipeBlock;
    public playerController playerScript;

    void OnTriggerEnter2D(Collider2D other)
    {
        playerScript.setPlayerInPipe(true);
        // Form a solid to block the player with cheese
       if(playerScript.playerCarryingCheese())
       {
           pipeBlock.isTrigger = false;
       }
       else
       {
           pipeBlock.isTrigger = true;
       }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        playerScript.setPlayerInPipe(false);
    }
}

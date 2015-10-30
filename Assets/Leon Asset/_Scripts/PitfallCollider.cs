using UnityEngine;
using System.Collections;

public class PitfallCollider : MonoBehaviour
{
    public playerController playerScript;

    void OnTriggerEnter2D(Collider2D other)
    {
        playerScript.anim.SetBool("MouseFell", true);
        playerScript.allowMovement(false);
        playerScript.setPlayerDied(true);
    }
}

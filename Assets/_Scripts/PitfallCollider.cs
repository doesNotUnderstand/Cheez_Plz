using UnityEngine;
using System.Collections;

public class PitfallCollider : MonoBehaviour
{
    public playerController playerScript;
    bool isLevel1 = false;

    void Start()
    {
        if (Application.loadedLevel == 2)
        {
            isLevel1 = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Equals("Mouse"))
        {
            playerScript.anim.SetBool("MouseFell", true);
            playerScript.allowMovement(false);
            playerScript.setPlayerDied(true);

            if (isLevel1)
            {
                GameObject.Find("Cheese Ball").GetComponent<cheeseBall>().resetPosition();
            }
        }
    }
}

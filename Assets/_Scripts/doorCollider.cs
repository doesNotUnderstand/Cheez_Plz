using UnityEngine;
using System.Collections;

public class doorCollider : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Mouse")
        {
            Destroy(this.gameObject);
        }
    }
}

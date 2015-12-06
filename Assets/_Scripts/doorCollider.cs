using UnityEngine;
using System.Collections;

public class doorCollider : MonoBehaviour {

    public pit_activator pitScript;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Mouse")
        {
            if (pitScript)
                pitScript.toggle_controller();
            Destroy(this.gameObject);
        }
    }
}

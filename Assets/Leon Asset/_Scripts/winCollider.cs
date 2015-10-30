using UnityEngine;
using System.Collections;

public class winCollider : MonoBehaviour {

    public SpriteRenderer dimRenderer; // For testing alpha effects
    public GameObject levelComplete;
    public bool dimLights = false;
    public int menuDelay = 5;
    float dimAlpha = 0.0f;
    bool playerInside = false;
    bool cheeseInside = false;

    void Start()
    {
        dimRenderer.color = new Color(1f, 1f, 1f, 0f);
        dimRenderer.gameObject.SetActive(true);
        levelComplete.SetActive(false);
    }

    void Update()
    {
        if(playerInside && cheeseInside)
        {
            dimLights = true;
        }
        dimLighting();
    }

    // Function that dims the light to show Victory message
    void dimLighting()
    {
        if (dimLights && dimAlpha < 0.5f)
        {
            dimAlpha += 0.25f * Time.deltaTime;
            dimRenderer.color = new Color(1f, 1f, 1f, dimAlpha);
        }
        else if(dimAlpha >= 0.5f)
        {
            levelComplete.SetActive(true);
            StartCoroutine(returnToMenu());
        }
    }

    // To wait certain amount of seconds and return player to main menu
    IEnumerator returnToMenu()
    {
        yield return new WaitForSeconds(menuDelay);
        Application.LoadLevel("Menu");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Mouse")
        {
            playerInside = true;
        }
        else if (other.name == "cheese")
        {
            cheeseInside = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Mouse")
            playerInside = false;
        else if (other.name == "cheese")
            cheeseInside = false;
    }
}

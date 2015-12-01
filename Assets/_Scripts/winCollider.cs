using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class winCollider : MonoBehaviour {

    public SpriteRenderer dimRenderer; // For testing alpha effects
    public GameObject levelComplete;
    public GameObject minimap; // Turn off minimap when level is complete
    public GameObject scoreMenu; // Manage the score display
    public mainCameraScript timerScript; // To stop time when player wins
    public Sprite cheeseRating; // Image of colored cheese
    public bool dimLights = false;
    public int menuDelay;
    public int level = 0;    

    ScoreSystem scoreScript;
    float dimAlpha = 0.0f;
    bool playerInside = false;
    bool cheeseInside = false;
    bool saved = false;
    bool scoreShown = false;

    void Start()
    {        
        scoreScript = timerScript.GetComponent<ScoreSystem>();
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

        if(scoreShown)
        {
            if(Input.anyKey)
            {
                StartCoroutine(returnToMenu());
            }
        }
        dimLighting();
    }    

    // Function that dims the light to show Victory message
    void dimLighting()
    {        
        if (dimLights && dimAlpha < 0.5f)
        {
            if (timerScript.triggerStatus()) // Stop timer
                timerScript.triggerTimer(false);
            if(minimap.activeSelf) // Turn off minimap
                minimap.SetActive(false);
            if(!saved)
            {
                scoreScript.save_score(level);
                saved = true;
            }
            dimAlpha += 0.35f * Time.deltaTime;
            dimRenderer.color = new Color(1f, 1f, 1f, dimAlpha);
        }
        else if(dimAlpha >= 0.5f)
        {          
            // Screen is now dark, display level complete, score, and cheese rating
            levelComplete.SetActive(true);

            // Set the time on score
            scoreMenu.GetComponent<Transform>().Find("txt_time").GetComponent<Text>().text 
                = timerScript.get_levelTimeMinutes() + ":" + timerScript.get_levelTimeSeconds();
            
            // Update the saved score if necessary
            string levelKey = scoreScript.returnLevelString(level);
            if ((PlayerPrefs.HasKey(levelKey) && PlayerPrefs.GetInt(levelKey) > timerScript.get_levelTime())
                 || !PlayerPrefs.HasKey(levelKey))
            {
                scoreMenu.GetComponent<Transform>().Find("txt_newhighscore").GetComponent<GameObject>().SetActive(true);
            }

            // Update cheese rating
            setCheeseImages(scoreScript.returnCheeseRating(level, timerScript.get_levelTime()));

            scoreMenu.SetActive(true);
            scoreShown = true;
        }
    }

    // To wait certain amount of seconds and return player to main menu
    IEnumerator returnToMenu()
    {
        yield return new WaitForSeconds(menuDelay);		
        Application.LoadLevel("Menu");
    }

    // Change cheese images depending on cheese rating
    void setCheeseImages(int rating)
    {
        if(rating == 3)
            scoreMenu.GetComponent<Transform>().Find("img_rating3").GetComponent<Image>().sprite = cheeseRating;                
        if(rating >= 2)
            scoreMenu.GetComponent<Transform>().Find("img_rating2").GetComponent<Image>().sprite = cheeseRating;                
        if(rating >= 1)
            scoreMenu.GetComponent<Transform>().Find("img_rating1").GetComponent<Image>().sprite = cheeseRating;    
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

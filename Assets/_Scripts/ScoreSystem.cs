using UnityEngine;
using System.Collections;

public class ScoreSystem : MonoBehaviour {

    //Public Variables        
    private mainCameraScript timerScript;

    void Start () 
	{
        timerScript = GetComponent<mainCameraScript>();
	}

    public void save_score(int level)
    {
        int score = timerScript.get_levelTime();
        string levelKey = "";

        if(level == 0)
        {
            levelKey = "L0Score";
        }
        else if(level == 1)
        {
            levelKey = "L1Score";
        }
        else if(level == 2)
        {
            levelKey = "L2Score";
        }

        if(levelKey != "")
        {
            if (PlayerPrefs.HasKey(levelKey))
            {
                if (PlayerPrefs.GetInt(levelKey) > score)
                    PlayerPrefs.SetInt(levelKey, score);
            }
            else
            {
                PlayerPrefs.SetInt(levelKey, score);
            }
        }
    }
}

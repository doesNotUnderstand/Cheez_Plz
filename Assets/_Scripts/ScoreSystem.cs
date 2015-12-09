using UnityEngine;
using System.Collections;

public class ScoreSystem : MonoBehaviour {

    public int[] tutorialRecord = { 60, 80, 100 }; // Cheese rating from hard to easy
    public int[] firstRecord = { 120, 240, 300 };
    public int[] secondRecord = { 300, 480, 600 };

    mainCameraScript timerScript;
    int totalLevels = 3; // change this value if more levels are created

    void Start()
    {
        timerScript = GetComponent<mainCameraScript>();
    }

    public void save_score(int level)
    {
        int score = timerScript.get_levelTime();
        string levelKey = "";

        if (level == 0)
        {
            levelKey = "L0Score";
        }
        else if (level == 1)
        {
            levelKey = "L1Score";
        }
        else if (level == 2)
        {
            levelKey = "L2Score";
        }

        if (levelKey != "")
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

    // Return the cheese rating depending on time cleared
    public int returnCheeseRating(int level, int time)
    {
        int[] ratingArray;

        if (level == 2) ratingArray = secondRecord;
        else if (level == 1) ratingArray = firstRecord;
        else ratingArray = tutorialRecord;

        for (int i = 0; i < totalLevels; i++)
        {
            if (time != 0 && time < ratingArray[i])
                return 3 - i;
        }
        return 0;
    }

    // Return a string representation of int level
    public string returnLevelString(int level)
    {
        if (level == 0)
        {
            return "L0Score";
        }
        else if (level == 1)
        {
            return "L1Score";
        }
        else if (level == 2)
        {
            return "L2Score";
        }
        else
            return "";
    }

    // Return the int seconds value of string key
    public int returnStoredScore(string key)
    {
        return PlayerPrefs.GetInt(key);
    }
}

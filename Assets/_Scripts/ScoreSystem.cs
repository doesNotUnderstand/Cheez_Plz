using UnityEngine;
using System.Collections;

public class ScoreSystem : MonoBehaviour {

    //Public Variables
    public int score;
	public string player_name;

    void Start () 
	{
		if (PlayerPrefs.HasKey (player_name))
		{
			score = PlayerPrefs.GetInt (player_name);
		} 
		else 
		{
			score = 0;
		}
	}

    public void update_score(int points)
    {
        score += points;
    }
    public void save_score()
    {
        PlayerPrefs.SetInt(player_name, score);
    }


}

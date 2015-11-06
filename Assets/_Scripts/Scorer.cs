using UnityEngine;
using System.Collections;

public class Scorer : MonoBehaviour {
	public int score;
	public string player_name;
	public void OnGUI()
	{
		GUI.Box (new Rect (10, 10, 100, 20), score.ToString ());
	}

	public void update_score(int points)
	{
		score += points;
	}
	public void save_score()
	{
		PlayerPrefs.SetInt (player_name, score);
	}
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
	


}

using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {

	public GameObject control_img;	
	public GameObject menu;
	public GameObject second_menu;
	public GameObject minimap;
	bool paused = false;
	void Start()
	{
		menu.SetActive(false);
		second_menu.SetActive(false);
		control_img.SetActive(false);
	}
	public void ctrl()
	{
		control_img.SetActive(true);
		menu.SetActive(false);
		second_menu.SetActive(true);
	}
	public void rsm()
	{
		Time.timeScale = 1;
		paused = false;
		menu.SetActive(false);
	}
	public void back()
	{
		second_menu.SetActive(false);
		menu.SetActive(true);
		control_img.SetActive(false);
	}
	public void goto_menu()
	{
		Time.timeScale = 1;
		Application.LoadLevel(0);
	}
	public void rstrt()
	{
		Time.timeScale = 1;
		Application.LoadLevel(Application.loadedLevel);
	}
	void Update() {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if(!paused)
			{
				menu.SetActive(true);
				Time.timeScale = 0;
				paused = true;
				minimap.SetActive(false);
			}
			else
			{
				menu.SetActive(false);
				second_menu.SetActive(false);
				control_img.SetActive(false);
				Time.timeScale = 1;
				paused = false;
				minimap.SetActive(true);
			}
		} 
	}
}

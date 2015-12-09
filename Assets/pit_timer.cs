using UnityEngine;
using System.Collections;

public class pit_timer : MonoBehaviour {
	bool pit_active;
	public GameObject pit;
	float pit_invisible_time;
	public float activate_time;
	// Use this for initialization
	void Start () 
	{
		pit_active = true;
		pit_invisible_time = 0;
	}
	public void toggle_pit()
	{
		pit_active = !pit_active;
		pit.gameObject.SetActive(pit_active);
		pit_invisible_time = 0;
	}
	public void turn_off()
	{
		pit_active = false;
		pit_invisible_time = 0;
		pit.gameObject.SetActive(pit_active);
	}
    public void turn_on()
    {
        pit_active = true;
        pit_invisible_time = 0;
        pit.gameObject.SetActive(true);
    }
	public void disable()
	{
		pit.gameObject.SetActive (false);
	}
	// Update is called once per frame
	void Update () 
	{
		if (!pit_active)
		{
			pit_invisible_time += 1.05f * Time.deltaTime;
		}
		if(pit_invisible_time > activate_time)
		{
			toggle_pit();
			pit_invisible_time = 0;
		}
	}
}

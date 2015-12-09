using UnityEngine;
using System.Collections;

public class pit_activator : MonoBehaviour {
	public GameObject controller;
	public GameObject controller2;
	public GameObject controller3;
	public GameObject controller4;
	public GameObject controller5;
	public GameObject controller6;
	public GameObject controller7;
	public GameObject controller8;
	public GameObject controller9;
	public GameObject controller10;
	public GameObject controller11;
	public GameObject controller12;
	public GameObject controller13;
	public GameObject controller14;
	public GameObject controller15;
	public GameObject controller16;
	public GameObject controller17;
	public GameObject controller18;
	public GameObject controller19;
	public GameObject controller20;
	public GameObject controller21;
	public GameObject controller22;
	public GameObject controller23;
	public GameObject controller24;
	public GameObject controller25;
	public GameObject controller26;
	public GameObject controller27;
	public GameObject controller28;
	public GameObject controller29;
	public GameObject controller30;

	bool c1,c2,c3,c4,c5,c6,c7,c8,c9,c10,c11,c12,c13,c14,c15,c16,c17,c18,c19,c20,c21,c22,c23,
		c24,c25,c26,c27,c28,c29,c30;
	public float timer;
	public bool controller_on;
	bool puzzle_done;
	void Start()
	{
		timer = 0;
		controller_on = false;
		puzzle_done = false;
	}
	void OnTriggerEnter2D(Collider2D c)
	{
		if (!controller_on && !puzzle_done)
		{
			controller_on = !controller_on;
			gameObject.GetComponent<BoxCollider2D>().enabled = false;
			c1 = false;
			c2 = false;
			c3 = false;
			c4 = false;
			c5 = false;
			c6 = false;
			c7 = false;
			c8 = false;
			c9 = false;
			c10 = false;
			c11 = false;
			c12 = false;
			c13 = false;
			c14 = false;
			c15 = false;
			c16 = false;
			c17 = false;
			c18 = false;
			c19 = false;
			c20 = false;
			c21 = false;
			c22 = false;
			c23 = false;
			c24 = false;
			c25 = false;
			c26 = false;
			c27 = false;
			c28 = false;
			c29 = false;
			c30 = false;
		}
	}
	public void toggle_controller()
	{
		puzzle_done = true;
		controller_on = false;
		timer = 0;
		foreach (GameObject g in GameObject.FindGameObjectsWithTag("Controller"))
		{
			g.gameObject.GetComponent<pit_timer>().turn_off();
		}
	}
    public void reset_controller()
    {
        controller_on = false;
        timer = 0;
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Controller"))
        {
            g.gameObject.GetComponent<pit_timer>().turn_on();
        }
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        c1 = false;
        c2 = false;
        c3 = false;
        c4 = false;
        c5 = false;
        c6 = false;
        c7 = false;
        c8 = false;
        c9 = false;
        c10 = false;
        c11 = false;
        c12 = false;
        c13 = false;
        c14 = false;
        c15 = false;
        c16 = false;
        c17 = false;
        c18 = false;
        c19 = false;
        c20 = false;
        c21 = false;
        c22 = false;
        c23 = false;
        c24 = false;
        c25 = false;
        c26 = false;
        c27 = false;
        c28 = false;
        c29 = false;
        c30 = false;
    }
    void Update()
	{
		if (controller_on) {
			timer += 2.05f * Time.deltaTime;
		}
		if (timer > 0 && !c1) {
			c1 = !c1;
			controller.GetComponent<pit_timer> ().toggle_pit ();
			controller2.GetComponent<pit_timer> ().toggle_pit ();
		} else if (timer > 2 && !c2) {
			c2 = !c2;
			controller6.GetComponent<pit_timer> ().toggle_pit ();
			controller8.GetComponent<pit_timer> ().toggle_pit ();
		} else if (timer > 4 && !c3) {
			c3 = !c3;
			controller3.GetComponent<pit_timer> ().toggle_pit ();
			controller15.GetComponent<pit_timer> ().toggle_pit ();
		} else if (timer > 6 && !c4) {
			c4 = !c4;
			controller4.GetComponent<pit_timer> ().toggle_pit ();
			controller24.GetComponent<pit_timer> ().toggle_pit ();
			controller6.GetComponent<pit_timer> ().toggle_pit ();
			controller25.GetComponent<pit_timer> ().toggle_pit ();
		} else if (timer > 8 && !c5) {
			c5 = !c5;
			controller5.GetComponent<pit_timer> ().toggle_pit ();
		} else if (timer > 10 && !c6) {
			c6 = !c6;
			controller6.GetComponent<pit_timer> ().toggle_pit ();
		} else if (timer > 12 && !c7) {
			c7 = !c7;
			controller7.GetComponent<pit_timer> ().toggle_pit ();
		} else if (timer > 14 && !c8) {
			c8 = !c8;
			controller8.GetComponent<pit_timer> ().toggle_pit ();
		} else if (timer > 16 && !c9) {
			c9 = !c9;
			controller9.GetComponent<pit_timer> ().toggle_pit ();
		} else if (timer > 18 && !c10) {
			c10 = !c10;
			controller10.GetComponent<pit_timer> ().toggle_pit ();
		} else if (timer > 20 && !c11) {
			c11 = !c11;
			controller11.GetComponent<pit_timer> ().toggle_pit ();
		} else if (timer > 22 && !c12) {
			c12 = !c12;
			controller12.GetComponent<pit_timer> ().toggle_pit ();
		} else if (timer > 24 && !c13) {
			c13 = !c13;
			controller13.GetComponent<pit_timer> ().toggle_pit ();
		} else if (timer > 26 && !c14) {
			c14 = !c14;
			controller14.GetComponent<pit_timer> ().toggle_pit ();
		} else if (timer > 28 && !c15) {
			c15 = !c15;
			controller15.GetComponent<pit_timer> ().toggle_pit ();
		} else if (timer > 30 && !c16) {
			c16 = !c16;
			controller16.GetComponent<pit_timer> ().toggle_pit ();
		} else if (timer > 32 && !c17) {
			c17 = !c17;
			controller17.GetComponent<pit_timer> ().toggle_pit ();
		} else if (timer > 34 && !c18) {
			c18 = !c18;
			controller18.GetComponent<pit_timer> ().toggle_pit ();
		} else if (timer > 36 && !c19) {
			c19 = !c19;
			controller19.GetComponent<pit_timer> ().toggle_pit ();
		} else if (timer > 38 && !c20) {
			c20 = !c20;
			controller20.GetComponent<pit_timer> ().toggle_pit ();
		} else if (timer > 40 && !c21) {
			c21 = !c21;
			controller21.GetComponent<pit_timer> ().toggle_pit ();
		} else if (timer > 42 && !c22) {
			c22 = !c22;
			controller22.GetComponent<pit_timer> ().toggle_pit ();
		} else if (timer > 44 && !c23) {
			c23 = !c23;
			controller23.GetComponent<pit_timer> ().toggle_pit ();
		} else if (timer > 46 && !c24) {
			c24 = !c24;
			controller24.GetComponent<pit_timer> ().toggle_pit ();
		} else if (timer > 48 && !c25) {
			c25 = !c25;
			controller25.GetComponent<pit_timer> ().toggle_pit ();
		} else if (timer > 50 && !c26) {
			c26 = !c26;
			controller26.GetComponent<pit_timer> ().toggle_pit ();
		} else if (timer > 52 && !c27) {
			c27 = !c27;
			controller27.GetComponent<pit_timer> ().toggle_pit ();
		} else if (timer > 54 && !c28) {
			c28 = !c28;
			controller28.GetComponent<pit_timer> ().toggle_pit ();
		} else if (timer > 56 && !c29) {
			c29 = !c29;
			controller29.GetComponent<pit_timer> ().toggle_pit ();
		} else if (timer > 58 && !c30) {
			c30 = !c30;
			controller30.GetComponent<pit_timer> ().toggle_pit ();
		}
	}	
}

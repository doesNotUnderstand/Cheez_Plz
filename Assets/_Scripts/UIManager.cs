using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

	public void StartGame()
    {
        Application.LoadLevel("Intro");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

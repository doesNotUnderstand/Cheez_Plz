using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

    public ScoreSystem scoreScript;

    // Main menu buttons
    public GameObject startButton;
    public GameObject continueButton;
    public GameObject settingsButton;
    public GameObject quitButton;

    // Settings buttons
    public GameObject controlButton;
    public GameObject creditsButton;
    public GameObject backButton;

    // Controls buttons
    public GameObject controlImage;
    public creditScroller creditScript;
    public GameObject backSettingButton;

    // Level select buttons
    public GameObject tutorialButton;
    public GameObject level1Button;
    public GameObject level2Button;
    public GameObject level3Button;
    public Sprite cheeseZero;
    public Sprite cheeseOne;
    public Sprite cheeseTwo;
    public Sprite cheeseThree;


    void Start()
    {
        if(sceneHolder.showCredits)
        {
            sceneHolder.showCredits = false;
            ShowSettings();
            ShowCredits();
        }
    }

	public void StartGame()
    {
        Application.LoadLevel("Intro");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadTutorial()
    {
        Application.LoadLevel("Intro");
    }

    public void LoadLevel1()
    {
        Application.LoadLevel("Level1");
    }

    public void LoadLevel2()
    {
        Application.LoadLevel("Level2");
    }

    public void LoadLevel3()
    {
        Application.LoadLevel("Level3");
    }

    // Set start, continue, settings, quit buttons to inactive and show settings and back button
    public void ShowSettings()
    {
        startButton.SetActive(false);
        continueButton.SetActive(false);
        settingsButton.SetActive(false);
        quitButton.SetActive(false);
        controlImage.SetActive(false);
        creditScript.startScript = false;
        backSettingButton.SetActive(false);
        
        controlButton.SetActive(true);
        creditsButton.SetActive(true);
        backButton.SetActive(true);
    }

    // Set control, credits, back buttons to inactive and show controls and backSetting button
    public void ShowControls()
    {
        controlButton.SetActive(false);
        creditsButton.SetActive(false);
        backButton.SetActive(false);

        // Display controls image here
        controlImage.SetActive(true);
        backSettingButton.SetActive(true);
    }

    // Set control, credits, back buttons to inactive and show credits and backSetting button
    public void ShowCredits()
    {
        controlButton.SetActive(false);
        creditsButton.SetActive(false);
        backButton.SetActive(false);

        // Display credits here
        creditScript.startScript = true;
        backSettingButton.SetActive(true);
    }

    // Sets all buttons to inactive and shows start, quit, control buttons
    public void ReturnToMain()
    {
        backButton.SetActive(false);
        controlButton.SetActive(false);
        creditsButton.SetActive(false);
        tutorialButton.SetActive(false);
        level1Button.SetActive(false);
        level2Button.SetActive(false);
        //level3Button.SetActive(false);

        startButton.SetActive(true);
        continueButton.SetActive(true);
        settingsButton.SetActive(true);
        quitButton.SetActive(true);
    }

    // Set start, quit buttons to inactive and show level buttons
    public void SelectLevels()
    {     
        startButton.SetActive(false);
        continueButton.SetActive(false);
        settingsButton.SetActive(false);
        quitButton.SetActive(false);

        setRatingImages();
        tutorialButton.SetActive(true);
        level1Button.SetActive(true);
        level2Button.SetActive(true);
        //level3Button.SetActive(true);
        backButton.SetActive(true);
    }

    void setRatingImages()
    {
        GameObject[] levelButtons = { tutorialButton, level1Button, level2Button };
        int rating = 0;
        for (int i = 0; i < levelButtons.Length; i++)
        {
            rating = scoreScript.returnCheeseRating(i,
                scoreScript.returnStoredScore(scoreScript.returnLevelString(i)));
            if (rating == 3)
                levelButtons[i].GetComponent<Image>().sprite = cheeseThree;
            else if (rating == 2)
                levelButtons[i].GetComponent<Image>().sprite = cheeseTwo;
            else if (rating == 1)
                levelButtons[i].GetComponent<Image>().sprite = cheeseOne;
            else
                levelButtons[i].GetComponent<Image>().sprite = cheeseZero;
        }
    }
}

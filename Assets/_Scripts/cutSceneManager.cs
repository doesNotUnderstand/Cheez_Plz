using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class sceneHolder
{
    static public int scene = 2;
    static public bool loadScene = false;
}

public class cutSceneManager : MonoBehaviour {
     
    public Sprite cutScene1;
    public Sprite cutScene2;    
    public SpriteRenderer renderer;
    public float dimmingSpeed;    

    bool loadingScene = false;
    float dimAlpha = 0.0f; 

	// Use this for initialization
	void Start ()
    {
        if(sceneHolder.loadScene)
        {
            loadingScene = true;
            renderer.color = new Color(1f, 1f, 1f, 0f);
            setScene(sceneHolder.scene);
        }          
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(loadingScene)
        {
            dimScene();
            if (Input.anyKeyDown)
            {
                dimAlpha = 1;
            }
        }
        else if(sceneHolder.loadScene)
        {
            if(Input.anyKeyDown)
            {
                Application.LoadLevel(sceneHolder.scene+2);
            }
        }
	}

    // Set the initial sprite cutscene depending on scene int
    void setScene(int sceneNum)
    {
        if (sceneNum == 0)
        {
            renderer.sprite = cutScene1;
        }
        else if (sceneNum == 1)
        {
            renderer.sprite = cutScene2;
        }
    }

    void dimScene()
    {
        dimAlpha += dimmingSpeed * Time.deltaTime;
        renderer.color = new Color(1f, 1f, 1f, dimAlpha);

        if (dimAlpha > 1)
            loadingScene = false;        
    }
}

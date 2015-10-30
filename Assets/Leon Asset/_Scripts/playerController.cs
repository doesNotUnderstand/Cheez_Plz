using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {

    // Be sure to freeze Z Contraint on Rigidbody 2D!!
    public float originalSpeed;
    float speed;
    public Animator anim;
    public cheeseCollider cheeseScript;
    public EventText textBox;
    Transform playerTransform;
    bool canMove; // Enable/Disable player movement
    bool playerInsidePipe; // To prevent the player from carrying cheese through pipe
    bool isCarryingCheese; // To check for cheese when player goes through pipe
    bool playerHasKey;
    bool playerDied;
    bool isCrouching;

    // For decoupling the player movement - Other scripts can move the character if needed
    bool playerMoveUp, playerMoveDown, playerMoveLeft, playerMoveRight;

    void Start()
    {
        anim = GetComponent<Animator>();
        playerTransform = GetComponent<Transform>();
        playerMoveUp = playerMoveDown = playerMoveLeft = playerMoveRight = false;
        canMove = false; // Initially the player cannot move
        playerHasKey = false; // Initially the player doesn't possess the key
        speed = originalSpeed;
        textBox.changeTimedText("Ham ham~", 10.0f);
    }

    void FixedUpdate()
    {
        playerMoveUp = playerMoveDown = playerMoveLeft = playerMoveRight = false;
        
        // Allow player to quit at any time
        if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (canMove)
        {
            // Separate left/right with up/down to enable diagonal movement
            if (Input.GetKey(KeyCode.A))
            {
                moveCharacterLeft();
            }
            else if (Input.GetKey(KeyCode.D))
            {
                moveCharacterRight();
            }
            
            if (Input.GetKey(KeyCode.W))
            {
                moveCharacterUp();
            }
            else if (Input.GetKey(KeyCode.S))
            {
                moveCharacterDown();
            }

            // Hold the Ctrl key to crouch and walk silently
            if (Input.GetKey(KeyCode.LeftControl))
            {
                isCrouching = true;
                speed = 0.75f * originalSpeed;
            }
            else
            {
                isCrouching = false;
            }

            // Hold the mouse left button to carry the cheese
            if (Input.GetKey(KeyCode.Mouse0) && cheeseScript.getCheeseRange() && !playerInsidePipe)
            {
                playerCarryCheese();
                isCarryingCheese = true;
                speed = 0.75f * originalSpeed;
            }
            else
            {
                isCarryingCheese = false;
            }

            // Return player to normal speed if not crouching or holding cheese
            if (!isCrouching && !isCarryingCheese)
            {
                speed = originalSpeed;
            }
        }
        
        handlePlayerAnimations();
    }

    // Method to set whether movement is allowed
    public void allowMovement(bool move)
    {
        canMove = move;
    }

    // Check whether the player is crouching
    public bool playerIsCrouching()
    {
        return isCrouching;
    }

    // Method to check whether the player has the key
    public bool playerHasGateKey()
    {
        return playerHasKey;
    }

    // Check if player is carrying cheese
    public bool playerCarryingCheese()
    {
        return isCarryingCheese;
    }

    // Returns whether the player is in pipe
    public void setPlayerInPipe(bool inPipe)
    {
        playerInsidePipe = inPipe;
    }

    // Method to set whether the player got/used the key
    public void playerKeyState(bool key)
    {
        playerHasKey = key;
    }

    // Sets whether the player died or not
    public void setPlayerDied(bool died)
    {
        playerDied = died;
    }

    // Return whether the player has died
    public bool getPlayerDied()
    {
        return playerDied;
    }

    // This function sets the character to hold the cheese
    void  playerCarryCheese()
    {
        cheeseScript.transform.position = playerTransform.transform.position;
    }

    // These functions moves the character and sets the boolean to be used
    // with the handlePlayerAnimations() function
    void moveCharacterLeft()
    {
        playerTransform.position += Vector3.left * speed * Time.deltaTime;
        playerMoveLeft = true;
    }

    void moveCharacterRight()
    {
        playerTransform.position += Vector3.right * speed * Time.deltaTime;
        playerMoveRight = true;
    }

    void moveCharacterUp()
    {
        playerTransform.position += Vector3.up * speed * Time.deltaTime;
        playerMoveUp = true;
    }

    void moveCharacterDown()
    {
        playerTransform.position += Vector3.down * speed * Time.deltaTime;
        playerMoveDown = true;
    }

    // This function sets the correction animation based on boolean values
    void handlePlayerAnimations()
    {
        if (playerMoveLeft)
        {
            anim.SetInteger("Direction", 1);
        }
        else if (playerMoveRight)
        {
            anim.SetInteger("Direction", 3);
        }
        else if (playerMoveUp)
        {
            anim.SetInteger("Direction", 2);
        }
        else if (playerMoveDown)
        {
            anim.SetInteger("Direction", 4);
        }
        else
        {
            anim.SetInteger("Direction", 0);
        }
    }
}

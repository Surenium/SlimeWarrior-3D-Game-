using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour

{

    private bool isActive = true;
    //Player Movement Script
    private IMovement movement;
    //Inventory
    private Inventory playerInventory;
    private Health playerHealth;
    // Start is called before the first frame update
    
    
    


    void Start()
{
        //Set the player to active
        GameManager.instance.SetPlayerController(this);
        SetupPlayer();


  
    }




// Update is called once per frame
void Update()
{
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.instance.GamePaused(true);
        }
       
    //check that the player is active
        if (!isActive)
        {
            return;
        }
    //Get the inputs
GetInput();
    }

//Set Up the player values and references
public void SetupPlayer()
{
    //Set Player Active
    isActive = true;

        //Components
   
    movement = GetComponent<IMovement>();
    movement.Setup();
    playerInventory = GetComponent<Inventory>();
    playerHealth = GetComponent<Health>();
    //update the coins
    playerInventory.AddCoin(0);
    }
// Get the Input from the Players controlls
public void GetInput()
{
    //local Variables
    Vector3 moveDirection = Vector3.zero;
    //Get PLayer Input
    moveDirection.x = Input.GetAxis("Horizontal");
    moveDirection.z = Input.GetAxis("Vertical");
    //Clean up the movement
    if (moveDirection.magnitude > 1)
        {
        moveDirection = moveDirection.normalized;
        }
    if (moveDirection.magnitude < 0.1f)
        {
        moveDirection = Vector3.zero;
        }
    //Assign a Jump
    bool jump = Input.GetButtonDown("Jump");
    //Move the player
//TODO:Move the player

    movement.UpdateMove(moveDirection, jump);
    
}
//Collectable Pickup Method
public void Collect(CollectableType collectableType, int value)
{
    //if this is a coin
    if (collectableType == CollectableType.Coin)
            {
            //Make sure there is an inventory
        if (playerInventory != null)
                {
                //add the coin
                    playerInventory.AddCoin(value);
            }
        }
    }



public Health GetHealth() => playerHealth;

public Inventory GetInventory() => playerInventory;


    public void YouWin()
    {
        GameManager.instance.YouWinSequence();
    }
    public void OnDeath()
    {
        GameManager.instance.GameOverSequence();

    }



    public void Damage(int amount)
    {
        playerHealth.Damage(amount);
    }
        
}

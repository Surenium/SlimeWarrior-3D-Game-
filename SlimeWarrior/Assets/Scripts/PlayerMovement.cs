using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]

public class PlayerMovement : MonoBehaviour, IMovement
{

    private Rigidbody moveRb;
    private Vector3 moveDirection = Vector3.zero;
    private bool isJumping = false;
    [SerializeField] private int numberOfJumps = 1;
    private int jumpsRemaining;
    private bool canDoubleJump { get { return jumpsRemaining > 0; } }
    private bool isGrounded = true;
    public float speed = 1.0f;
    public float turnSpeed = 15.0f;
    public float jumpHeight = 10.0f;
    private PlayerController controller;
    private Animator anim;


    //FixedUpdate gets called every .02 seconds and is used for Physics updates
    
    public void FixedUpdate()
    {
        //we have input
        if (moveDirection != Vector3.zero)
        {

            MoveThePlayer();

        }
        if (isJumping)
        {
            Jump();
        }

    }



    private void Start()
    {
        controller = GetComponent<PlayerController>();
        anim = GetComponentInChildren<Animator>();

        
    }
    private void Jump()
    {
        if (!isGrounded)
        {
            DoubleJump();
            return;
        }
        moveRb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        Debug.Log("Trying to jump");
        isJumping = false;
        isGrounded = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            run();
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            Idle();
        }
    }
    private void DoubleJump()
    {
        //see if we are allowed to double jump
        if (!canDoubleJump)
        {
            return;
        }
        //iterate over our jumps available
        jumpsRemaining--;
        //apply the force
        moveRb.AddForce(Vector3.up* jumpHeight, ForceMode.Impulse);
        isJumping = false;
    }

  

   


    private void MoveThePlayer()
    {
        //Get the current Position
        Vector3 currentPosition = transform.position;
        //get the local direction for the movement we want to do
        Vector3 transformMoveDirection = transform.TransformDirection(moveDirection);
        //Find a step in our speed that would take given time that has passed
        Vector3 step = currentPosition + transformMoveDirection * Time.fixedDeltaTime * speed;
        //Step to that new position
        moveRb.MovePosition(step);
        // Look direction from where we are, to where we want to go
        Quaternion lookDirection = Quaternion.LookRotation(transformMoveDirection.normalized);
        Quaternion lookStep = Quaternion.Slerp(transform.rotation, lookDirection, Time.fixedDeltaTime * turnSpeed);
        //Rotate the player to look at that position
        moveRb.MoveRotation(lookStep);
        

    }

    private void Attack()
    {

        anim.SetTrigger("Attack");
    }
   
    private void run()
    {
        anim.SetFloat("Speed", 1);
    }

    private void Idle()
    {
        anim.SetFloat("Speed", 0);
    }

  



    public float GetCurrentSpeed()
    {
        return 1;
    }
    public void Setup()

    {
        //Get The Player's Rigidbody
        moveRb = GetComponent<Rigidbody>();
        jumpsRemaining = numberOfJumps;

    }


    //input values are coming from the player to the movement
    public void UpdateMove(Vector3 directionToMove, bool tryJump)
    {
        //Store the movement input from the player to apply in fixedUpdate
        moveDirection = directionToMove;
        if (tryJump)
        {
            this.isJumping = true;
        }
    }



    public void OnDeath()
    {
    
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;

            jumpsRemaining = numberOfJumps;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;

           
        }
    }


}
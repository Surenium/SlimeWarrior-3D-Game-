using System.Collections;
using UnityEngine;
using UnityEngine.AI;
//Required Components
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class EnemyController : MonoBehaviour
{
    //EnemyScalars
   
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float rotationSpeed = 5.0f;
    [SerializeField] private float stoppingDistance = 1.0f;
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private int attackDamage = 10;
    [SerializeField] private float updateCoolDown = 0.1f;
    //EnemyCompoents
    private NavMeshAgent agent;
    //enemyBooleans
    private bool isActive = false;
    //Look rotations
    private Quaternion lookRotation;
    //for animation
    private Animator anim;


    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        GameManager.instance.GameStartedEvent += SetupEnemy;
        GameManager.instance.PauseGameEvent += PauseEnemy;
        //Register to start with the Game Manager

        

    }

   
    private void Update()
    {
        if (!isActive) return;
        //Rotate towards the destination
        RotateTowardsDestination();
    }

    private void RotateTowardsDestination()
    {
        //direction to the destination
        Vector3 direction = agent.destination - transform.position;
        if (direction != Vector3.zero)
            {
            //set the look rotation
                lookRotation = Quaternion.LookRotation(direction);
                //Rotation
                float rotation = rotationSpeed * Time.deltaTime;
                //rotate the enemy
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotation);
            }
    
    }


    //Set up the Enemy
    private void SetupEnemy()
    {
        isActive = true;
        //Set the agent
        agent = GetComponent<NavMeshAgent>();
            //set the stopping distance
        agent.stoppingDistance = stoppingDistance;
            //set the speed
        agent.speed = speed;
            //set the rotation speed
        agent.angularSpeed = rotationSpeed;

        
            //Start looking for the player

        StartCoroutine(LookForPlayer());
            //start the core behaviour
        StartCoroutine(EnemyBehaviour());
    
    }

    private void PauseEnemy(bool isPaused)
    {
        isActive = !isPaused;
        if (isActive)
        {
            SetupEnemy();
            agent.isStopped = false;

        }
        else 
        {
            agent.isStopped = true;
        }

    }
    private IEnumerator LookForPlayer()
    {
        
        while (isActive)
       {

                            
                yield return new WaitForSeconds(updateCoolDown);
                SetDestination();
            }
        
            
        }
    //Set the destination
    private void SetDestination()
    {
        //get the player
        PlayerController player = GameManager.instance.playerController;
        if (player is null) return;

        //check the distance to the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= 15f) //change 15f to the desired distance
        {
            //set the destination to the player's position
            agent.destination = player.gameObject.transform.position;
        }
    }

    //Enemy Behaviour
    private IEnumerator EnemyBehaviour()
    {
        //Set The Destination
        SetDestination();
        yield return null;
        //Check if the enemy is active
        while (isActive)
            {
            
                SetDestination();
                //check if the enemy is in range
           
            
                Vector3 playerPosition = GameManager.instance.playerController.transform.position;
                float distance = Vector3.Distance(playerPosition, transform.position);
        
                if (distance <= stoppingDistance)
                    {
                    //Attack
                   
                        Attack();
                    
                    
                    //Wait for the attack cooldown
                        yield return new WaitForSeconds(attackCooldown);
                        continue;
                    }
                    //wait for the update cooldown
                    yield return new WaitForSeconds(updateCoolDown);
                    //set the destination
                
                }
        }

   

    //Attack the player
    private void Attack()
    {
        
        GameManager.instance.playerController.Damage(attackDamage);
        anim.SetTrigger("EnemyAttack");

    }

    public void DamagePlayer()
    {

    }

}

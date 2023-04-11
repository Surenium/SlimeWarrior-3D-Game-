using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    //Character Scalars
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private int healthRegenRate = 1;
    [SerializeField] private int healthRegenDelay = 1;

    //Character Booleans
    private bool isDead;
    private bool isRegenerating;
    private bool isDamaged;

    //Start is called before the first frame update
    private void Start()
    {
        
    }

    //Damage the character
    public void Damage(int damage)
    {
        //Check if the cahracter is dead
        if (isDead)
        {
            return;
        }

        //Substract the damage from the current health
        currentHealth -= damage;
        GameManager.instance.UpdateHealth(currentHealth);
        //Check if the character is dead
        if (currentHealth <= 0)
        {

            //set the isDead boolean
            isDead = true;
            //Call the die  method
            Die();
        }
    }

    public int GetMaxHealth() => maxHealth;

    public int GetCurrentHealth() => currentHealth;



    //Trigger Death on the character
    private void Die()
        {

        //Send the death message to the player
        GameManager.instance.GameOverSequence();
        }
    }


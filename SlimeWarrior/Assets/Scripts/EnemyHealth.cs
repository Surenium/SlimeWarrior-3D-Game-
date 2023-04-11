using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{

    public float health;
    public float maxHealth;
  

    public GameObject healthBarUI;
    public Slider slider;

    

    private void Start()
    {
        health = maxHealth;
        slider.value = CalculateHealth();
    }

    private void Update()
    {
        slider.value = CalculateHealth();

        if(health < maxHealth)
        {
            healthBarUI.SetActive(true);

        }


        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }

    float CalculateHealth()
    {
        return (health / maxHealth)*100;
    }

    //change Health when damage is taken
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}

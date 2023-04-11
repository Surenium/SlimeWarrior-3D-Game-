using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDemo : MonoBehaviour
{
    [SerializeField] private int damageAmount;

    private void OnCollisionEnter(Collision collision)
    {
       
    

        PlayerController controller = collision.gameObject.GetComponent<PlayerController>();
        if(controller != null)
        {
            controller.Damage(damageAmount);
        }
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private int damageAmount;
    private bool isLeftMouseClicked = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isLeftMouseClicked = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isLeftMouseClicked = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isLeftMouseClicked && gameObject.activeSelf)
        {
            if (collision.collider.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damageAmount);
            }
        }
    }
}
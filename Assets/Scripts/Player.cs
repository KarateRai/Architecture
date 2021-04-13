using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int current_Health;
    public int max_Health = 100;
    public int current_Attack = 1;
    public int pickup_amount = 0;

    public HealthBar healthBar;
    public Pickup pickup;

    void Start()
    {
        current_Health = max_Health;
        healthBar.SetMaxHealth(max_Health);
    }

    //TEMP: Just to test the healthbar function.
    void TakeDamage(int damage)
    {
        current_Health -= damage;

        healthBar.SetHealth(current_Health);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "pickup")
        {
            Destroy(collision.gameObject);
            pickup.AddPickups();
        }
    }
}

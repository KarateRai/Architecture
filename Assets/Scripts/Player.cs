using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Player
{
    public static int current_Health = 100;
    public static int max_Health = 100;
    public static int current_Attack = 1;
    public static int pickup_amount = 0;

    public static HealthBar_UI healthBar;
    public static Pickup_UI pickup;

    public static void AddPoints(int i)
    {
        pickup_amount += i;
    }

    public static void ResetPoints()
    {
        pickup_amount = 0;
    }

    //TEMP: Just to test the healthbar function.
    public static void TakeDamage(int damage)
    {
        current_Health -= damage;

        healthBar.SetHealth(current_Health);
    }
}

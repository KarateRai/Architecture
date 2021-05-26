using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupObject : MonoBehaviour
{
    private bool isUsed = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isUsed != true)
        {
            Player.AddPoints(1);
            isUsed = true;
            FindObjectOfType<AudioManager>().Play("coin");
            GameObject.Destroy(gameObject);
        }
    }
}

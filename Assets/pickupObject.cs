using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject.Destroy(gameObject);
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    //Add score to UI
    //    //Debug.Log("Trying to destroy");
    //    GameObject.Destroy(gameObject);
    //}
}

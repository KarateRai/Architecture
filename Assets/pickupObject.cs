using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupObject : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Add score to UI
        Debug.Log("Trying to destroy");
        GameObject.Destroy(gameObject);
    }
}

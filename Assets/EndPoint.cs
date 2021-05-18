using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndPoint : MonoBehaviour
{
    [SerializeField] UnityEvent clearStage;
    private bool stageCleared = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (stageCleared == false)
        {

            clearStage.Invoke();
            stageCleared = true;
        }
    }
}

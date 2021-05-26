using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonOnStateChange : MonoBehaviour, ISelectHandler
{
    public UnityEvent onSelected;

    public void OnSelect(BaseEventData eventData)
    {
        onSelected.Invoke();
    }
}

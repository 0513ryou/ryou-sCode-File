using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PlayerInputButton : MonoBehaviour, IPointerDownHandler
{
    public UnityEvent OnButtonDown;


    // Start is called before the first frame update
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(this.gameObject.name + " Was Clicked.");
        OnButtonDown.Invoke();
    }
}

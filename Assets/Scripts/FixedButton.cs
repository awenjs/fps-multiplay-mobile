using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class FixedButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public bool Pressed { get; private set; }
    void OnDisable() => Pressed = false;
    public void OnPointerDown( PointerEventData eventData ) => Pressed = true;
    public void OnPointerUp( PointerEventData eventData ) => Pressed = false;
}

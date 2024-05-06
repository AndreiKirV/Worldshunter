using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Temp : MonoBehaviour, IPointerEnterHandler
{
    //EventTrigger
    private void OnMouseDown()
    {
        Debug.Log($"Кликнули по UI {this.gameObject.name}");
    }

    public void Deb(string message)
    {
        Debug.Log($"{this.gameObject.name}: {message}");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log($"{this.gameObject.name}: OnPointerEnter");
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIEventer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Data _data;

    public void SetData()
    {
        _data = Data.GetInstance();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _data.IsUiFree = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _data.IsUiFree = true;
    }
}
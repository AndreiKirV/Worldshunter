using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardMB : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RectTransform RectTransform;
    public Transform ParentTransform;
    public Transform TemporaryParentTransform;
    public List<Element> _elements = new List<Element>()
    {
        new Element("Cost"),
        new Element("Health"),
        new Element("Armour"),
        new Element("Speed"),
        new Element("Attack"),
        new Element("Lable"),
        new Element("Description"),
        new Element("Special"),
        new Element("Portrait")
    };

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log($"Enter {this.name}");
        transform.parent = TemporaryParentTransform;
        RectTransform.sizeDelta = new Vector2(300, 400);
        RectTransform.anchoredPosition = new Vector2(RectTransform.anchoredPosition.x, 0);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log($"Exit {this.name}");
        RectTransform.sizeDelta = new Vector2(150, 200);
        transform.parent = ParentTransform;
    }

    [Serializable]
    public sealed class Element
    {
        public string Name;
        public GameObject GameObject;
        public TextMeshProUGUI Text;
        public Image Icon;

        public Element(string name)
        {
            Name = name;
        }
    }
}
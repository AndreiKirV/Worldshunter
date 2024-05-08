using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardMB : MonoBehaviour
{
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
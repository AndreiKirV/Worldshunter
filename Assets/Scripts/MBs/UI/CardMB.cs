using Client;
using Leopotam.EcsLite;
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
    public Transform TemporaryParentTransform;

    private Action _actionPointerExit;

    private EcsWorld _world;

    public List<Element> Elements = new List<Element>()
    {
        new Element(Data.Glossary.CardElements.COST),
        new Element(Data.Glossary.CardElements.HEALTH),
        new Element(Data.Glossary.CardElements.ARMOUR),
        new Element(Data.Glossary.CardElements.SPEED),
        new Element(Data.Glossary.CardElements.ATTACK),
        new Element(Data.Glossary.CardElements.LABLE),
        new Element(Data.Glossary.CardElements.DESCRIPTION),
        new Element(Data.Glossary.CardElements.SPECIAL),
        new Element(Data.Glossary.CardElements.PORTRAIT)
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

    public void SetCFG(CardSO cfg)
    {
        foreach (var item in Elements)
        {
            //switch

        }
    }

    public void SetActionPointerExit(Action action)
    {
        _actionPointerExit += action;
    }

    public void SetWorld(EcsWorld world) 
    {
        _world = world;    
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_world != null)
        {
            int entity = _world.NewEntity();
            ref CardHoverComp cardComp = ref _world.GetPool<CardHoverComp>().Add(entity);
            cardComp.MB = this;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _actionPointerExit?.Invoke();
    }
}
using Client;
using Leopotam.EcsLite;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardMB : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RectTransform RectTransform;
    public Transform TemporaryParentTransform;
    public RectTransform _viewRect;

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
        new Element(Data.Glossary.CardElements.PORTRAIT),
        new Element(Data.Glossary.CardElements.DISTANCE)
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

    public void SetCfg(CardSO cfg)
    {
        foreach (var item in Elements)
        {
            switch (item.Name)
            {
                case Data.Glossary.CardElements.COST:
                    item.Text.text = $"{cfg.Cost}";
                    break;
                case Data.Glossary.CardElements.HEALTH:
                    item.Text.text = $"{cfg.Health}";
                    break;
                case Data.Glossary.CardElements.ARMOUR:
                    item.Text.text = $"{cfg.Armour}";
                    break;
                case Data.Glossary.CardElements.SPEED:
                    item.Text.text = $"{cfg.Speed}";

                    if(cfg.Speed == 0)
                        item.GameObject.SetActive(false);

                    break;
                case Data.Glossary.CardElements.ATTACK:
                    item.Text.text = $"{cfg.Damage}";

                    if (cfg.AttacType == Data.Glossary.CardElements.AttackType.None)
                        item.GameObject.SetActive(false);
                    else if (cfg.AttacType == Data.Glossary.CardElements.AttackType.Melee)
                        item.Icon.sprite = Data.GetInstance().ImageManager.MeleeAttackIcon;
                    else if (cfg.AttacType == Data.Glossary.CardElements.AttackType.Ranged)
                        item.Icon.sprite = Data.GetInstance().ImageManager.RangedAttackIcon;

                    break;
                case Data.Glossary.CardElements.LABLE:
                    item.Text.text = $"{cfg.Name}";
                    break;
                case Data.Glossary.CardElements.DESCRIPTION:
                    item.Text.text = $"{cfg.Description}";
                    break;
                case Data.Glossary.CardElements.SPECIAL://доделать если решу ввести спешелы, на нг
                    break;
                case Data.Glossary.CardElements.PORTRAIT:
                    item.Icon.sprite = cfg.PortraitView;
                    break;
                case Data.Glossary.CardElements.DISTANCE:
                    item.Text.text = $"{cfg.AttackDistance}";

                    if (cfg.AttacType == Data.Glossary.CardElements.AttackType.None)
                        item.GameObject.SetActive(false);

                    break;
            }

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
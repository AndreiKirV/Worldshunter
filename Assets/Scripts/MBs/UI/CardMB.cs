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

    public RectTransform RectTransform;
    public Transform TemporaryParentTransform;
    public bool IsPointerEnter = true;

    private EcsWorld _world;

    private Action _actionPointerExit;

    public List<Element> Elements = new List<Element>()
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

        /* transform.parent = TemporaryParentTransform;
        RectTransform.sizeDelta = new Vector2(300, 400);
        RectTransform.anchoredPosition = new Vector2(RectTransform.anchoredPosition.x, 0); */
        /*
        if (IsPointerEnter)
        {
            Debug.Log($"Enter {this.name}");
            _view = Instantiate(this, transform.position, Quaternion.identity, TemporaryParentTransform);
            _view.IsPointerEnter = false;
            _view.SetActionPointerExit(() => Destroy(_view.gameObject));
            _view.RectTransform.sizeDelta = new Vector2(300, 400);
            _view.RectTransform.anchoredPosition = new Vector2(RectTransform.anchoredPosition.x, _view.RectTransform.sizeDelta.y / 2);

            if(Data.GetInstance().View == null)
            {
                Data.GetInstance().View = _view.gameObject;
            }
            else
            {
                Destroy(Data.GetInstance().View);
                Data.GetInstance().View = _view.gameObject;
            }

            if(_view.RectTransform.anchoredPosition.x < _view.RectTransform.sizeDelta.x)
            {
                _view.RectTransform.anchoredPosition = new Vector2(_view.RectTransform.sizeDelta.x / 2, _view.RectTransform.anchoredPosition.y);
            }
            else if(_view.RectTransform.anchoredPosition.x > Screen.width - _view.RectTransform.sizeDelta.x)
            {
                _view.RectTransform.anchoredPosition = new Vector2(Screen.width - _view.RectTransform.sizeDelta.x, _view.RectTransform.anchoredPosition.y);
            }
        }
        */


    }

    public void OnPointerExit(PointerEventData eventData)
    {
        /* RectTransform.sizeDelta = new Vector2(150, 200);
        transform.parent = ParentTransform; */
        //Destroy(_view.gameObject);
        Debug.Log($"Exit {this.name}");
        _actionPointerExit?.Invoke();
    }
}
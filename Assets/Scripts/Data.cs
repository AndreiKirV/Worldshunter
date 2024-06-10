using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Client;
using OpenCover.Framework.Model;
using Unity.VisualScripting;
using UnityEngine;


[Serializable]
public class Data
{
    [Header("Настройки карты")]
    public Map Map = new Map();
    [Header("Настройки гравитации")]
    public Gravity Gravity = new Gravity();
    [Header("Сборщик изображений")]
    public ImageCollector ImageCollector = new ImageCollector();
    [Header("Сборщик префабов")]
    public PrefCollector PrefCollector = new PrefCollector();
    [Header("Сборщик UI")]
    public UICollector UICollector = new UICollector();
    [Header("Конфиги игровых карт")]
    public List<CardSO> CardsSO;

    public Data()
    {
        instance = this;
    }

    private static Data instance;

    public static Data GetInstance()
    {
        instance ??= new Data();
        return instance;
    }

    public static class Glossary
    {
        public static class CardElements
        {
            public const string COST = "COST";
            public const string HEALTH = "HEALTH";
            public const string ARMOUR = "ARMOUR";
            public const string SPEED = "SPEED";
            public const string ATTACK = "ATTACK";
            public const string LABLE = "LABLE";
            public const string DESCRIPTION = "DESCRIPTION";
            public const string SPECIAL = "SPECIAL";
            public const string PORTRAIT = "PORTRAIT";
            public const string DISTANCE = "DISTANCE";

            public enum AttackType
            {
                None, Ranged, Melee
            }
        }
    }
}

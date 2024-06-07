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
    public Map Map = new Map();
    public Gravity Gravity = new Gravity();
    public ImageCollector ImageManager = new ImageCollector();
    //скомпоновать, когда придет время
    public Camera Camera;
    public GameObject Canvas;

    public GameChipMB GameChipPref;

    public Transform CardTemporaryParentTransform;
    public Transform CardsContentTransform;
    public CardMB CardMBPref;

    public int MaxSpawnChip;
    public bool IsUiFree = true;

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

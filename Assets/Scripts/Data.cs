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
    //скомпоновать, когда придет время
    public Camera Camera;
    public GameObject Canvas;

    public GameChipMB GameChipPref;

    public Transform CardTemporaryParentTransform;
    public Transform CardsContentTransform;
    public CardMB CardMBPref;

    public int MaxSpawnChip;
    public int MaxTileIsOneByOne;
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
            public static readonly string COST = "COST";
            public static readonly string HEALTH = "HEALTH";
            public static readonly string ARMOUR = "ARMOUR";
            public static readonly string SPEED = "SPEED";
            public static readonly string ATTACK = "ATTACK";
            public static readonly string LABLE = "LABLE";
            public static readonly string DESCRIPTION = "DESCRIPTION";
            public static readonly string SPECIAL = "SPECIAL";
            public static readonly string PORTRAIT = "PORTRAIT";
        }
    }
}

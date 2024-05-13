using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Client;
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
    public GameObject View;

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
}

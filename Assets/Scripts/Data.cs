using System;
using System.Collections;
using System.Collections.Generic;
using Client;
using Unity.VisualScripting;
using UnityEngine;


[Serializable]
public class Data
{
    public Map Map = new Map();

    public Data()
    {
    }
    //пока синглтон не нужен, может вдруг 
    /* private static Data instance;

    public static Data GetInstance()
    {
        if (instance == null)
            instance = new Data();
        return instance;
    }

    private Data()
    {

    } */
}

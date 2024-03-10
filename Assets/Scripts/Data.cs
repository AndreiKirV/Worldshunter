using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Saves
{
    [Serializable]
    public class Data
    {
        private static Data instance;
        public List<TileMB> Tiles = new List<TileMB>();
        public int V = 0;

        public static Data GetInstance()
        {
            if (instance == null)
                instance = new Data();
            return instance;
        }

        private Data()
        {

        }
    }
}
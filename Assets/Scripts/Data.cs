using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Saves
{
    [Serializable]
    public class Data
    {
        private static Data instance;
        public List<TileMB> Tiles;

        public static Data getInstance()
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
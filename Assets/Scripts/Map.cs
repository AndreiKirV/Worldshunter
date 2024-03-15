using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    public class Map
    {
        public TileMB TilePref;

        public PlaygroundMB Playground;

        public Vector2 MaxPosition;
        public Vector2 MinPosition;
        
        public float TilePosY;

        public List<TileMB> Tiles = new List<TileMB>();
    }
}
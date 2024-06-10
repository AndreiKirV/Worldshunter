using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Client
{
    [Serializable]
    public class Map
    {
        public PlaygroundMB Playground;
        
        public Vector2 MaxPosition;
        public Vector2 MinPosition;

        public float TileStartPosY;
        public float GameChipStartPosY;

        public int MaxSpawnChip;

        public List<TileMB> Tiles = new List<TileMB>();

        public void SetDeltaPos(Vector2 target)
        {
            if (target.x > MaxPosition.x && target.y > MaxPosition.y)
                MaxPosition = target;
            else if (target.x < MinPosition.x && target.y < MinPosition.y)
                MinPosition = target;
        }
    }
}
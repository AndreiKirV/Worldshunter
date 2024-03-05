using Leopotam.EcsLite;
using UnityEngine;

namespace Client
{
    sealed class PlaygroundInitSystem : IEcsInitSystem
    {
        private TileMB _tilePref;

        public void Init(IEcsSystems systems)
        {
            PlaygroundMB playground = GameObject.FindObjectOfType<PlaygroundMB>();

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    TileMB tempTile = Object.Instantiate<TileMB>(_tilePref, playground.transform);
                    tempTile.transform.localPosition = new Vector3(i, 0, j);
                    tempTile.name = $"{_tilePref.name} [{i}]:[{j}]";
                }
            }
        }

        public PlaygroundInitSystem(TileMB pref)
        {
            _tilePref = pref;
        }
    }
}
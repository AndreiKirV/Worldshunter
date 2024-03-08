using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class TileGravitySystems : IEcsRunSystem
    {
        private EcsFilterInject<Inc<TileComp>> _tileFilter = default;
        private EcsPoolInject<TileComp> _tilePool;
        private float _speed = 1;
        private TileMB _curTile;

        public void Run(IEcsSystems systems)
        {
            foreach (var entityTimer in _tileFilter.Value)
            {
                TileComp tempTileComp = _tilePool.Value.Get(entityTimer);

                if (_curTile == null)
                    _curTile = tempTileComp.MB;

                if (_curTile.Transform.localPosition.y > 0)
                    _curTile.Transform.localPosition = new UnityEngine.Vector3(_curTile.Transform.localPosition.x, _curTile.Transform.localPosition.y - Time.deltaTime * _speed, _curTile.GameObject.transform.localPosition.z);
                else
                {
                    _curTile.Mesh.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                    _curTile = null;
                }

            }
        }
    }
}
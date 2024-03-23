using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class TileGravitySystems : IEcsRunSystem
    {
        private EcsCustomInject<Data> _data = default;

        private EcsFilterInject<Inc<TileComp, MustFallComp>, Exc<GameChipComp>> _tileFilter = default;
        private EcsFilterInject<Inc<TileComp>, Exc<MustFallComp>> _tileInstalledFilter = default;

        private EcsPoolInject<TileComp> _tilePool;
        private EcsPoolInject<MustFallComp> _mustFallPool;
        private EcsPoolInject<SpawnByGameChipEvent> _spawnByGameChipEventPool;
        private int _actuationCountIsOneByOne = 0;

        public void Run(IEcsSystems systems)
        {
            bool isProcessing = false;
            bool tileIsEnable = true;


            foreach (var entityTile in _tileFilter.Value)
            {
                if (tileIsEnable)
                {
                    foreach (var entityInstalledTile in _tileInstalledFilter.Value)
                    {
                        ref TileComp tileComp = ref _tilePool.Value.Get(entityInstalledTile);
                        tileComp.MB.BoxCollider.enabled = false;
                    }

                    tileIsEnable = false;
                }

                if (!isProcessing)
                {
                    if (_data.Value.Gravity.IsOneByOne && _actuationCountIsOneByOne < _data.Value.MaxTileIsOneByOne)//TODO
                        isProcessing = true;

                    ref TileComp tempTileComp = ref _tilePool.Value.Get(entityTile);

                    if (tempTileComp.MB.Transform.localPosition.y > 0)
                    {
                        tempTileComp.MB.Transform.localPosition = new Vector3(tempTileComp.MB.Transform.localPosition.x, tempTileComp.MB.Transform.localPosition.y - Time.deltaTime * _data.Value.Gravity.Scale, tempTileComp.MB.Transform.localPosition.z);
                    }
                    else
                    {
                        tempTileComp.MB.Mesh.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                        tempTileComp.MB.Transform.localPosition = new Vector3(tempTileComp.MB.Transform.localPosition.x, 0, tempTileComp.MB.GameObject.transform.localPosition.z);
                        _mustFallPool.Value.Del(tempTileComp.MB.Entity);

                        if (tempTileComp.MB.Pos == Vector2.zero)
                        {
                            if (!_spawnByGameChipEventPool.Value.Has(entityTile))
                                _spawnByGameChipEventPool.Value.Add(entityTile);
                        }

                        if (_tileFilter.Value.GetEntitiesCount() == 1 || !_data.Value.Gravity.IsOneByOne)//TODO когда последний тайл приземлился
                        {
                            foreach (var entityInstalledTile in _tileInstalledFilter.Value)
                            {
                                ref TileComp tileComp = ref _tilePool.Value.Get(entityInstalledTile);
                                tileComp.MB.BoxCollider.enabled = true;
                                tileIsEnable = true;
                            }

                            _actuationCountIsOneByOne++;
                        }

                        if (_data.Value.Gravity.IsOneByOne && _actuationCountIsOneByOne >= _data.Value.MaxTileIsOneByOne)//TODO
                        {
                            foreach (var entityInstalledTile in _tileInstalledFilter.Value)
                            {
                                ref TileComp tileComp = ref _tilePool.Value.Get(entityInstalledTile);
                                tileComp.MB.BoxCollider.enabled = true;
                                tileIsEnable = true;
                            }
                        }
                    }
                }
                else
                    continue;
            }
        }
    }
}
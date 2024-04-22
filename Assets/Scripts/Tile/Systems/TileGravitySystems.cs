using System.Collections.Generic;
using System.Diagnostics;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Debug = UnityEngine.Debug;

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
        private float _velocityMultiplier = 1;

        public void Run(IEcsSystems systems)
        {
            bool isProcessing = false;

            foreach (var entityTile in _tileFilter.Value)
            {
                if (!isProcessing)
                {
                    if (_data.Value.Gravity.IsOneByOne && _actuationCountIsOneByOne < _data.Value.MaxTileIsOneByOne)//TODO
                        isProcessing = true;

                    ref TileComp tempTileComp = ref _tilePool.Value.Get(entityTile);

                    if (tempTileComp.MB.Transform.localPosition.y > 0)
                    {
                        Vector3 curPos = tempTileComp.MB.Transform.localPosition;
                        _velocityMultiplier += Time.deltaTime;
                        float targetPosY = tempTileComp.MB.Transform.localPosition.y - Time.deltaTime * _data.Value.Gravity.Scale * _velocityMultiplier;
                        tempTileComp.MB.Transform.localPosition = new Vector3(curPos.x, targetPosY, curPos.z);

                        if (targetPosY < 0)//узкое место
                        {
                            tempTileComp.MB.Mesh.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                            tempTileComp.MB.Transform.localPosition = new Vector3(curPos.x, 0, curPos.z);
                            _mustFallPool.Value.Del(tempTileComp.MB.Entity);

                            if (tempTileComp.MB.Pos == Vector2.zero)
                            {
                                if (!_spawnByGameChipEventPool.Value.Has(entityTile))
                                    _spawnByGameChipEventPool.Value.Add(entityTile);
                            }

                            //TODOкогда тайлы падают все вместе, отрабатывает при приземелении
                            if (_data.Value.Gravity.IsOneByOne && _actuationCountIsOneByOne >= _data.Value.MaxTileIsOneByOne)//TODO
                            {
                                foreach (var entityInstalledTile in _tileInstalledFilter.Value)
                                {
                                    ref TileComp tileComp = ref _tilePool.Value.Get(entityInstalledTile);
                                    tileComp.MB.BoxCollider.enabled = true;
                                }

                                _velocityMultiplier = 1;
                            }
                            //TODO когда последний тайл приземлился, если летят по одному
                            else if (_tileFilter.Value.GetEntitiesCount() == 1 || !_data.Value.Gravity.IsOneByOne)
                            {
                                foreach (var entityInstalledTile in _tileInstalledFilter.Value)
                                {
                                    ref TileComp tileComp = ref _tilePool.Value.Get(entityInstalledTile);
                                    tileComp.MB.BoxCollider.enabled = true;
                                }

                                _actuationCountIsOneByOne++;
                                _velocityMultiplier = 1;
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
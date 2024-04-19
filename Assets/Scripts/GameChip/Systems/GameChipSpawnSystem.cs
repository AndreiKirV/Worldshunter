using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class GameChipSpawnSystem : SpawnSystem
    {
        private EcsFilterInject<Inc<SpawnByGameChipEvent, TileComp>, Exc<MustFallComp>> _spawnByGameChipFilter = default;

        private EcsPoolInject<TileComp> _tilePool;
        private EcsPoolInject<SpawnByGameChipEvent> _spawnByGameChipEventPool;
        private EcsPoolInject<GameChipComp> _gameChipCompPool;
        private EcsPoolInject<MustFallComp> _mustFallCompPool;

        override public void Run(IEcsSystems systems)
        {
            foreach (var entity in _spawnByGameChipFilter.Value)
            {
                SpawnByGameChipEvent spawnByGameChipEvent = _spawnByGameChipEventPool.Value.Get(entity);

                ref TileComp tileComp = ref _tilePool.Value.Get(entity);

                if (tileComp.GameChip == null)
                {
                    ref GameChipComp gameChipComp = ref _gameChipCompPool.Value.Add(entity);
                    
                    tileComp.GameChip = StartSpawn(tileComp.MB.Transform, _data.Value.GameChipPref);
                    gameChipComp.ZeroPositionY = tileComp.GameChip.transform.localPosition.y;
                    gameChipComp.MB = tileComp.GameChip;

                    tileComp.GameChip.Transform.localPosition = new Vector3(0, _data.Value.Map.GameChipPosY, 0);
                    ref MustFallComp mustFallComp = ref _mustFallCompPool.Value.Add(entity);
                    mustFallComp.Transform = gameChipComp.MB.transform;

                    gameChipComp.Level = 1;
                }
                else
                {
                    ref GameChipComp gameChipComp = ref _gameChipCompPool.Value.Get(entity);

                    if (gameChipComp.Level < _data.Value.MaxSpawnChip)
                    {
                        tileComp.GameChip = StartSpawn(tileComp.MB.Transform, _data.Value.GameChipPref);
                        gameChipComp.ZeroPositionY = tileComp.GameChip.transform.localPosition.y + gameChipComp.Level;
                        gameChipComp.MB = tileComp.GameChip;

                        tileComp.GameChip.Transform.localPosition = new Vector3(0, _data.Value.Map.GameChipPosY, 0);
                        ref MustFallComp mustFallComp = ref _mustFallCompPool.Value.Add(entity);
                        mustFallComp.Transform = gameChipComp.MB.transform;
                    }

                    gameChipComp.Level++;
                    //Debug.Log($"GameChipSpawnSystem: запросили спавн GameChip в {tileComp.MB.Pos} {gameChipComp.Level}");
                }

                _spawnByGameChipEventPool.Value.Del(entity);
            }
        }
    }
}
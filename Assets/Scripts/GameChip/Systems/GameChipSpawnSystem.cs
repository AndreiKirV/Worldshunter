using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class GameChipSpawnSystem : SpawnSystem
    {
        private EcsFilterInject<Inc<SpawnByGameChipEvent, TileComp>> _spawnByGameChipFilter = default;

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
                    tileComp.GameChip = StartSpawn(tileComp.MB.Transform, _data.Value.GameChipPref);

                    ref GameChipComp gameChipComp = ref _gameChipCompPool.Value.Add(entity);
                    gameChipComp.Level = 1;

                    gameChipComp.ZeroPositionY = tileComp.GameChip.transform.localPosition.y;
                    
                    gameChipComp.MB = tileComp.GameChip;

                    ref MustFallComp mustFallComp = ref _mustFallCompPool.Value.Add(entity);
                    mustFallComp.Transform = gameChipComp.MB.transform;

                    tileComp.GameChip.Transform.localPosition = new Vector3(0, _data.Value.Map.GameChipPosY, 0);
                }
                else
                {
                    tileComp.GameChip = StartSpawn(tileComp.MB.Transform, _data.Value.GameChipPref);

                    ref GameChipComp gameChipComp = ref _gameChipCompPool.Value.Get(entity);

                    gameChipComp.ZeroPositionY = tileComp.GameChip.transform.localPosition.y + gameChipComp.Level;
                    gameChipComp.Level++;

                    gameChipComp.MB = tileComp.GameChip;

                    ref MustFallComp mustFallComp = ref _mustFallCompPool.Value.Add(entity);
                    mustFallComp.Transform = gameChipComp.MB.transform;

                    tileComp.GameChip.Transform.localPosition = new Vector3(0, _data.Value.Map.GameChipPosY, 0);
                }

                Debug.Log($"GameChipSpawnSystem: запросили спавн GameChip в {tileComp.MB.Pos}");
                _spawnByGameChipEventPool.Value.Del(entity);
            }
        }
    }
}
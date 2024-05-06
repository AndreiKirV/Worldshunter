using System.Diagnostics;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Debug = UnityEngine.Debug;

namespace Client
{
    sealed class EventSystem : IEcsRunSystem
    {
        private EcsWorldInject _world = default;

        private EcsFilterInject<Inc<ClickTileEvent, TileComp>> _clickTileEventFilter = default;

        private EcsPoolInject<TileComp> _tilePool;
        private EcsPoolInject<SpawnByTileEvent> _spawnByTileEventPool;
        private EcsPoolInject<SpawnByGameChipEvent> _spawnByGameChipEventPool;
        private EcsPoolInject<ClickTileEvent> _clickTileEventPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _clickTileEventFilter.Value)
            {
                TileComp tileComp = _tilePool.Value.Get(entity);
                
                if(!_spawnByTileEventPool.Value.Has(entity))
                _spawnByTileEventPool.Value.Add(entity);//TODO эвент спавна тайла

                if(!_spawnByGameChipEventPool.Value.Has(entity))
                _spawnByGameChipEventPool.Value.Add(entity);//TODO эвент спавна игровоко айтима

                //Debug.Log($"EventSystem: Кликнули на тайл {tileComp.MB.Pos}");
                //Конец эвента
               _clickTileEventPool.Value.Del(entity);
            }
        }
    }
}
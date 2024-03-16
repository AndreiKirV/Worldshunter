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
        private EcsPoolInject<ClickTileEvent> _clickTileEventPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _clickTileEventFilter.Value)
            {
                TileComp tileComp = _tilePool.Value.Get(entity);
                
                _spawnByTileEventPool.Value.Add(entity);//TODO Эвент спавна


                Debug.Log($"EventSystem: Кликнули на тайл {tileComp.MB.Pos}");
                //Конец эвента
               _clickTileEventPool.Value.Del(entity);
            }
        }
    }
}
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class TickSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<TickTimerEvent>> _tickFilter = default;
        private EcsFilterInject<Inc<TimerComp>> _timerCompFilter = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _tickFilter.Value)
            {
                Debug.Log(entity);
                _tickFilter.Pools.Inc1.Del(entity);
            }
        }
    }
}
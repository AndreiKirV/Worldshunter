using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed partial class TickSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<TickTimerEvent>> _tickFilter = default;
        private EcsFilterInject<Inc<TimerComp>> _timerCompFilter = default;
        private EcsPoolInject<TimerComp> _timerPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _tickFilter.Value)
            {
                _tickFilter.Pools.Inc1.Del(entity);

                foreach (var entityTimer in _timerCompFilter.Value)
                {
                    TimerComp tempTimerComp = _timerPool.Value.Get(entityTimer);
                    tempTimerComp.SetTime();
                }
            }
        }
    }
}
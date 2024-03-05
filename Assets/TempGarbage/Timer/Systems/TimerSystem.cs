using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class TimerSystem : IEcsRunSystem
    {
        private EcsWorldInject _world = default;
        private EcsPoolInject<TimerComp> _timerPool;
        private EcsPoolInject<TickTimerEvent> _tickPool;
        private EcsFilterInject<Inc<TimerComp>> _filter = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref TimerComp timerComp = ref _timerPool.Value.Get(entity);

                float tempValue = timerComp.Value;
                timerComp.Value += Time.deltaTime * timerComp.Speed;

                if ((int)timerComp.Value > (int)tempValue)
                {
                    ref TickTimerEvent tickEventComp = ref _tickPool.Value.Add(_world.Value.NewEntity());//TODO для запоминания евентов - не забыть убрать
                    //timerComp.ViewText.text = $"{(timerComp.Value / 60)} : {(timerComp.Value % 60)}";
                }
            }
        }
    }
}
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class TimerInitSystem : IEcsInitSystem
    {
        private EcsWorldInject _world = default;
        private EcsPoolInject <TimerComp> _timerPool;
        
        public void Init(IEcsSystems systems)
        {
            TimerMB timer = GameObject.FindObjectOfType<TimerMB>();
            int entity = _world.Value.NewEntity();

            ref TimerComp timerComp = ref _timerPool.Value.Add(entity);
            timerComp.MB = timer;
        }
    }
}
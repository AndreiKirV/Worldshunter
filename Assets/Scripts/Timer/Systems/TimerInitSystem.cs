using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client
{
    sealed class TimerInitSystem : IEcsInitSystem
    {
        private EcsWorldInject _world = default;
        
        public void Init(IEcsSystems systems)
        {
            // add your initialize code here.
        }
    }
}
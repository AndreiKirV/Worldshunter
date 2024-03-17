using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    public class SpawnSystem : IEcsRunSystem
    {
        protected EcsCustomInject<Data> _data = default;

        protected EcsWorldInject _world = default;

        virtual public void Run(IEcsSystems systems)
        {
        }

        public static T StartSpawn<T>(Transform transform, T pref) where T : Object
        {
            T tempObject = Object.Instantiate<T>(pref, transform);
            return tempObject;
        }
    }
}
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

        protected T StartSpawn<T>(Transform transform, T pref) where T : Object
        {
            T tempObject = Object.Instantiate<T>(pref, transform);
            return tempObject;
        }

        public static T StartSpawn<T>(Transform transform, T pref, Vector3 pos) where T : Object
        {
            T tempObject = Object.Instantiate<T>(pref, pos, Quaternion.identity, transform);
            return tempObject;
        }
    }
}
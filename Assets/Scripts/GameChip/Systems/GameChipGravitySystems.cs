using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class GameChipGravitySystems : IEcsRunSystem
    {
        private EcsCustomInject<Data> _data = default;

        private EcsFilterInject<Inc<GameChipComp, MustFallComp>> _gameChipFilter = default;

        private EcsPoolInject<GameChipComp> _gameChipCompPool;
        private EcsPoolInject<MustFallComp> _mustFallPool;

        public void Run(IEcsSystems systems)
        {

            foreach (var entityChip in _gameChipFilter.Value)
            {
                Debug.Log("есть MustFallComp");

                ref GameChipComp gameChipComp = ref _gameChipCompPool.Value.Get(entityChip);

                if (gameChipComp.MB.transform.localPosition.y > gameChipComp.ZeroPositionY)
                {
                    gameChipComp.MB.Transform.localPosition = new Vector3(0, gameChipComp.MB.Transform.localPosition.y - Time.deltaTime * _data.Value.Gravity.Scale, 0);
                }
                else
                {
                    Debug.Log("убрал MustFallComp");

                    gameChipComp.MB.Mesh.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                    gameChipComp.MB.Transform.localPosition = new Vector3(0, gameChipComp.ZeroPositionY, 0);
                    //gameChipComp.MB.BoxCollider.enabled = true;
                    _mustFallPool.Value.Del(entityChip);
                }
            }

        }
    }
}
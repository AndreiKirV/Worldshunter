using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class TileGravitySystems : IEcsRunSystem
    {
        private EcsFilterInject<Inc<TileComp, MustFallComp>> _tileFilter = default;
        private EcsFilterInject<Inc<TimerComp>> _timerFilter = default;
        private EcsPoolInject<TileComp> _tilePool;
        private EcsPoolInject<MustFallComp> _mustFallPool;
        private float _speed = 50;
        bool _isOneByOne = true;

        public void Run(IEcsSystems systems)
        {
            {
                // Создаем флаг для отслеживания состояния обработки элементов
                bool isProcessing = false;

                foreach (var entityTile in _tileFilter.Value)
                {
                    // Проверяем, не обрабатывается ли уже какой-либо элемент
                    if (!isProcessing)
                    {
                        // Устанавливаем флаг в true, чтобы указать, что мы начали обрабатывать текущий элемент
                        if(_isOneByOne)
                        isProcessing = true;

                        TileComp tempTileComp = _tilePool.Value.Get(entityTile);

                        if (tempTileComp.MB.Transform.localPosition.y > 0)
                        {
                            tempTileComp.MB.Transform.localPosition = new Vector3(tempTileComp.MB.Transform.localPosition.x, tempTileComp.MB.Transform.localPosition.y - Time.deltaTime * _speed, tempTileComp.MB.Transform.localPosition.z);
                        }
                        else
                        {
                            tempTileComp.MB.Mesh.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                            tempTileComp.MB.Transform.localPosition = new Vector3(tempTileComp.MB.Transform.localPosition.x, 0, tempTileComp.MB.GameObject.transform.localPosition.z);
                            _mustFallPool.Value.Del(tempTileComp.MB.Entity);
                        }
                    }
                    else
                    {
                        // Если флаг уже установлен в true, пропускаем обработку текущего элемента
                        continue;
                    }
                }
            }
        }
    }
}
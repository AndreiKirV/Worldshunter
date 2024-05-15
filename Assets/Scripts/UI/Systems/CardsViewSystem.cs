using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace Client
{
    sealed class CardsViewSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<CardHoverComp>> _cardHoverFilter = default;
        private EcsPoolInject<CardHoverComp> _cardHoverPool;

        private CardMB _currentView;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _cardHoverFilter.Value)
            {
                ref CardHoverComp comp = ref _cardHoverPool.Value.Get(entity);

                if (_currentView != null)
                    MonoBehaviour.Destroy(_currentView.gameObject);

                _currentView = SpawnSystem.StartSpawn(comp.MB.TemporaryParentTransform, comp.MB, comp.MB.transform.position);
                _currentView.SetActionPointerExit(() => MonoBehaviour.Destroy(_currentView.gameObject));
                _currentView.RectTransform.sizeDelta = new Vector2(300, 400);

                Vector2 targetPos;

                if (_currentView.RectTransform.anchoredPosition.x < _currentView.RectTransform.sizeDelta.x)
                    targetPos = new Vector2(_currentView.RectTransform.sizeDelta.x / 2, _currentView.RectTransform.sizeDelta.y / 2);
                else if (_currentView.RectTransform.anchoredPosition.x > Screen.width - _currentView.RectTransform.sizeDelta.x)
                    targetPos = new Vector2(Screen.width - _currentView.RectTransform.sizeDelta.x, _currentView.RectTransform.sizeDelta.y / 2);
                else
                    targetPos = new Vector2(comp.MB.RectTransform.anchoredPosition.x, _currentView.RectTransform.sizeDelta.y / 2);

                _currentView.RectTransform.anchoredPosition = targetPos;


                _cardHoverPool.Value.Del(entity);
            }
        }
    }
}
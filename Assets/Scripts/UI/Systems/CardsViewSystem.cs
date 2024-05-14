using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class CardsViewSystem : IEcsRunSystem
    {
        private EcsCustomInject<Data> _data = default;

        private EcsFilterInject<Inc<CardHoverComp>> _cardHoverFilter = default;
        private EcsPoolInject<CardHoverComp> _cardHoverPool;

        private CardMB _currentView;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _cardHoverFilter.Value)
            {
                ref CardHoverComp comp = ref _cardHoverPool.Value.Get(entity);
                
                _currentView = SpawnSystem.StartSpawn(comp.MB.TemporaryParentTransform, comp.MB, comp.MB.transform.position);
                _currentView.IsPointerEnter = false;
                _currentView.SetActionPointerExit(() => MonoBehaviour.Destroy(_currentView.gameObject));
                _currentView.RectTransform.sizeDelta = new Vector2(300, 400);
                _currentView.RectTransform.anchoredPosition = new Vector2(comp.MB.RectTransform.anchoredPosition.x, _currentView.RectTransform.sizeDelta.y / 2);

                if (Data.GetInstance().View == null)
                {
                    Data.GetInstance().View = _currentView.gameObject;
                }
                else
                {
                    MonoBehaviour.Destroy(Data.GetInstance().View);
                    Data.GetInstance().View = _currentView.gameObject;
                }

                if (_currentView.RectTransform.anchoredPosition.x < _currentView.RectTransform.sizeDelta.x)
                {
                    _currentView.RectTransform.anchoredPosition = new Vector2(_currentView.RectTransform.sizeDelta.x / 2, _currentView.RectTransform.anchoredPosition.y);
                }
                else if (_currentView.RectTransform.anchoredPosition.x > Screen.width - _currentView.RectTransform.sizeDelta.x)
                {
                    _currentView.RectTransform.anchoredPosition = new Vector2(Screen.width - _currentView.RectTransform.sizeDelta.x, _currentView.RectTransform.anchoredPosition.y);
                }
                
                _cardHoverPool.Value.Del(entity);
            }
        }
    }
}
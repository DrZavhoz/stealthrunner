using FunnyBlox;
using UnityEngine;

namespace Deslab
{
    public class UIFollower : MonoBehaviour
    {
        [SerializeField] private Transform _targetTransform;

        [SerializeField] private RectTransform _rectTransform;

        [SerializeField] private Vector2 _offsetPosition;

        private Camera _mainCamera;
        private Vector2 _screenPosition;

        private void OnEnable()
        {
            EventsService.OnGameStart += OnGameStart;
        }

        private void OnDisable()
        {
            EventsService.OnGameStart -= OnGameStart;
        }

        private void Start()
        {
            if (!_rectTransform)
                _rectTransform = GetComponent<RectTransform>();
            _mainCamera = Camera.main;
        }

        private void LateUpdate()
        {
            if (_targetTransform)
            {
                _screenPosition = _mainCamera.WorldToScreenPoint(_targetTransform.position);
                _rectTransform.position = _screenPosition + _offsetPosition;
            }
        }

        private void OnGameStart()
        {
            //_targetTransform = StaticManager.instance.levelManager.currentPlayer.transform.GetChild(3);
        }
    }
}
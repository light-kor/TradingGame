using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Settings;
using UnityEngine;
using Zenject;

namespace Core
{
    public class CameraMoveController
    {
        [Inject] private readonly AnimationSettings _animationSettings;
        [Inject] private readonly CoreEventBus _coreEventBus;
        [Inject] private readonly Camera _mainCamera;

        private TweenerCore<Vector3, Vector3, VectorOptions> _activeAnimation;
        private bool _isAnimationActive;

        public void MoveCameraInstantly(Vector3 candlePosition)
        {
            var targetPosition = ConvertCameraToCandlePosition(candlePosition);
            _mainCamera.transform.position = targetPosition;
            InvokeCameraMovedEvent();
        }

        private Vector3 ConvertCameraToCandlePosition(Vector3 candlePosition)
        {
            var xPos = candlePosition.x + _animationSettings.CameraMoveXOffset;
            var zPos = _mainCamera.transform.position.z;
            
            return new Vector3(xPos, candlePosition.y, zPos);
        }
        
        private void InvokeCameraMovedEvent()
        {
            _coreEventBus.FireCameraMoved();
        }

        public void MoveCameraWithAnimation(Vector3 candlePosition)
        {
            if (_isAnimationActive)
            {
                _activeAnimation.Kill();
                HandleAnimationFinish();
            }
            
            var targetPosition = ConvertCameraToCandlePosition(candlePosition);

            _isAnimationActive = true;

            _activeAnimation = _mainCamera.transform.DOMove(targetPosition, _animationSettings.CameraMoveDuration)
                .SetEase(_animationSettings.CameraMoveEase)
                .OnUpdate(InvokeCameraMovedEvent)
                .OnComplete(HandleAnimationFinish);
        }

        private void HandleAnimationFinish()
        {
            _activeAnimation = null;
            _isAnimationActive = false;
        }
    }
}
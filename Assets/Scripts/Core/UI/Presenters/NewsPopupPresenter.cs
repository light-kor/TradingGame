using System;
using Core.Candles;
using Core.UI.Providers;
using DG.Tweening;
using Settings;
using UnityEngine;
using Zenject;

namespace Core.UI.Presenters
{
    /// <summary>
    /// Презентер для отображения попапа новостей с анимацией
    /// </summary>
    public class NewsPopupPresenter : IDisposable
    {
        [Inject] private readonly NewsPopupProvider _newsPopupProvider;
        [Inject] private readonly AnimationSettings _animationSettings;
        [Inject] private readonly GameSettings _settings;
        
        private Sequence _currentAnimation;
        
        public void Dispose()
        {
            _currentAnimation?.Kill();
        }

        public void ShowNewsPopup(NewsCandlePriceSettings newsSettings)
        {
            Color directionColor = newsSettings.IsLong ? _settings.LongColor : _settings.ShortColor;
            
            _newsPopupProvider.SetDirectionColor(directionColor);
            _newsPopupProvider.SetTitle(newsSettings.Title);
            _newsPopupProvider.SetDescription(newsSettings.Description);
            
            _newsPopupProvider.SetActive(true);
            AnimateNewsPopup();
        }
        
        private void AnimateNewsPopup()
        {
            _currentAnimation?.Kill();
            _currentAnimation = DOTween.Sequence();
            
            var rectTransform = _newsPopupProvider.PopupRectTransform;
            Vector2 screenSize = new Vector2(Screen.width, Screen.height);
            
            // Начальная позиция (за левым краем экрана)
            Vector2 startPosition = new Vector2(-rectTransform.rect.width, rectTransform.anchoredPosition.y);
            
            // Конечная позиция (в центре нижней части экрана)
            Vector2 endPosition = new Vector2(0, rectTransform.anchoredPosition.y);
            
            // Позиция для выхода (за правым краем экрана)
            Vector2 exitPosition = new Vector2(screenSize.x + rectTransform.rect.width, rectTransform.anchoredPosition.y);
            
            // Устанавливаем начальную позицию
            rectTransform.anchoredPosition = startPosition;
            
            // Анимация входа
            _currentAnimation.Append(DOTween.To(() => rectTransform.anchoredPosition, x => rectTransform.anchoredPosition = x, endPosition, _animationSettings.NewsPopupSlideDuration)
                .SetEase(_animationSettings.NewsPopupSlideEase));
            
            // Задержка на экране
            _currentAnimation.AppendInterval(_animationSettings.NewsPopupDisplayDuration);
            
            // Анимация выхода
            _currentAnimation.Append(DOTween.To(() => rectTransform.anchoredPosition, x => rectTransform.anchoredPosition = x, exitPosition, _animationSettings.NewsPopupSlideDuration)
                .SetEase(_animationSettings.NewsPopupSlideEase));
            
            _currentAnimation.OnComplete(() =>
            {
                _newsPopupProvider.SetActive(false);
                _currentAnimation = null;
            });
        }
    }
} 
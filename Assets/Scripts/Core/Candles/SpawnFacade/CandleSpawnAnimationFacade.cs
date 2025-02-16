using System.Collections;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Settings;
using UnityEngine;
using Zenject;

namespace Core.Candles.SpawnFacade
{
    public class CandleSpawnAnimationFacade : CandleSpawnFacadeBase
    {
        [Inject] private readonly AnimationSettings _animationSettings;
        [Inject] private readonly CoreEventBus _coreEventBus;

        public IEnumerator AnimateCandle(CandlePresenter presenter)
        {
            float animDuration = _animationSettings.CandleAnimationDuration;
            var priceSettings = presenter.PriceSettings;
            var provider = presenter.Provider;

            // Расчёт размеров сегментов анимации на основе настроек свечи.
            float firstTargetScale;
            float secondTargetScale = 0f;
            float thirdTargetScale;
            float fourthTargetScale = Mathf.Abs(priceSettings.ClosePrice - priceSettings.OpenPrice);

            bool isLong = priceSettings.IsLong;

            if (isLong)
            {
                firstTargetScale = Mathf.Abs(priceSettings.LowPrice - priceSettings.OpenPrice);
                thirdTargetScale = Mathf.Abs(priceSettings.HighPrice - priceSettings.OpenPrice);
            }
            else
            {
                firstTargetScale = Mathf.Abs(priceSettings.HighPrice - priceSettings.OpenPrice);
                thirdTargetScale = Mathf.Abs(priceSettings.LowPrice - priceSettings.OpenPrice);
            }

            Sequence animSequence = DOTween.Sequence();

            float scaleMoveValue = firstTargetScale + firstTargetScale + thirdTargetScale + (thirdTargetScale - fourthTargetScale);
            float firstScaleTime = (firstTargetScale / scaleMoveValue) * animDuration;
            float secondScaleTime = firstScaleTime;
            float thirdScaleTime = (thirdTargetScale / scaleMoveValue) * animDuration;
            float fourthScaleTime = ((thirdTargetScale - fourthTargetScale) / scaleMoveValue) * animDuration;

            // Первая часть анимации: смена цвета и масштабирование вниз (или вверх) с фитилем.
            animSequence.AppendCallback(() => provider.SetColor(GetColorByDirection(isLong)));

            animSequence.Append(CreateScaleTween(presenter, firstTargetScale, !isLong, firstScaleTime));
            animSequence.AppendCallback(() => SetWickScaleSizeAndPosition(provider, firstTargetScale, !isLong));
            animSequence.Append(CreateScaleTween(presenter, secondTargetScale, !isLong, secondScaleTime));

            // Вторая часть анимации: смена цвета и масштабирование вверх (или вниз) с фитилем.
            animSequence.AppendCallback(() => provider.SetColor(GetColorByDirection(isLong)));

            animSequence.Append(CreateScaleTween(presenter, thirdTargetScale, isLong, thirdScaleTime));
            animSequence.AppendCallback(() => SetWickScaleSizeAndPosition(provider, thirdTargetScale, isLong, firstTargetScale));
            animSequence.Append(CreateScaleTween(presenter, fourthTargetScale, isLong, fourthScaleTime));

            animSequence.Play();

            yield return animSequence.WaitForCompletion();
        }

        private TweenerCore<Vector3, Vector3, VectorOptions> CreateScaleTween(CandlePresenter presenter,
            float targetScale, bool isAboveZero, float duration)
        {
            var provider = presenter.Provider;
            
            return provider.BodyTransform.DOScaleY(targetScale, duration)
                .SetEase(_animationSettings.CandleAnimationEase)
                .OnUpdate(() =>
                {
                    UpdateCurrentPrice(presenter, isAboveZero);
                    UpdateBodyPosition(provider, isAboveZero);
                });
        }

        protected override void UpdateCurrentPrice(CandlePresenter presenter, bool isAboveZero)
        {
            base.UpdateCurrentPrice(presenter, isAboveZero);
            _coreEventBus.FireCurrentPriceUpdated(presenter);
        }
    }
}
using System.Collections;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Settings;
using UnityEngine;
using Zenject;

namespace Core.Candles
{
    public class CandleAnimationFacade
    {
        [Inject] private readonly AnimationSettings _animationSettings;
        [Inject] private readonly GameSettings _settings;

        public IEnumerator AnimateCandle(CandlePresenter presenter, bool instantlySpawn)
        {
            float animDuration = instantlySpawn ? 0f : _animationSettings.CandleAnimationDuration;
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
            animSequence.AppendCallback(() => provider.SetColor(GetColorByDirection(!isLong)));

            animSequence.Append(CreateScaleTween(provider, firstTargetScale, !isLong, firstScaleTime));
            animSequence.AppendCallback(() => SetWickScaleSizeAndPosition(provider, firstTargetScale, !isLong));
            animSequence.Append(CreateScaleTween(provider, secondTargetScale, !isLong, secondScaleTime));

            // Вторая часть анимации: смена цвета и масштабирование вверх (или вниз) с фитилем.
            animSequence.AppendCallback(() => provider.SetColor(GetColorByDirection(!isLong)));

            animSequence.Append(CreateScaleTween(provider, thirdTargetScale, isLong, thirdScaleTime));
            animSequence.AppendCallback(() => SetWickScaleSizeAndPosition(provider, thirdTargetScale, isLong, firstTargetScale));
            animSequence.Append(CreateScaleTween(provider, fourthTargetScale, isLong, fourthScaleTime));

            animSequence.Play();

            yield return animSequence.WaitForCompletion();
        }

        private Color GetColorByDirection(bool isLong)
        {
            return !isLong ? _settings.LongColor : _settings.ShortColor;
        }

        private TweenerCore<Vector3, Vector3, VectorOptions> CreateScaleTween(CandleProvider provider,
            float targetScale, bool isAboveZero, float duration)
        {
            return provider.BodyTransform.DOScaleY(targetScale, duration)
                .SetEase(_animationSettings.CandleAnimationEase)
                .OnUpdate(() => UpdateBodyPosition(provider, isAboveZero));
        }

        private void UpdateBodyPosition(CandleProvider provider, bool isAboveZero)
        {
            float currentScale = Mathf.Abs(provider.BodyTransform.localScale.y);
            float halfHeight = currentScale / 2f;
            Vector3 newLocalPos = provider.BodyTransform.localPosition;

            // Если анимация растёт вверх – центр устанавливается на halfHeight, иначе – -halfHeight.
            newLocalPos.y = isAboveZero ? halfHeight : -halfHeight;
            provider.BodyTransform.localPosition = newLocalPos;
        }

        private void SetWickScaleSizeAndPosition(CandleProvider provider, float targetScale,
            bool isAboveZero, float additionalScale = 0f)
        {
            float currentScale = targetScale + additionalScale;

            Transform wickTransform = provider.WickTransform;
            SetWickLocalPosition(isAboveZero, wickTransform, targetScale, additionalScale);
            SetWickScale(wickTransform, currentScale);
        }

        private void SetWickLocalPosition(bool isAboveZero, Transform wickTransform, float targetScale, float additionalScale)
        {
            Vector3 newLocalPos = wickTransform.localPosition;

            // Для тени в другую сторону логика чуть сложнее, вот и разделение
            if (Mathf.Approximately(additionalScale, 0f))
            {
                float halfHeight = (targetScale + additionalScale) / 2f;
                newLocalPos.y = isAboveZero ? halfHeight : -halfHeight;
            }
            else
            {
                float halfHeight = targetScale / 2f;
                newLocalPos.y += isAboveZero ? halfHeight : -halfHeight;
            }

            wickTransform.localPosition = newLocalPos;
        }

        private void SetWickScale(Transform wickTransform, float currentScale)
        {
            Vector3 localScale = wickTransform.localScale;
            localScale.y = currentScale;
            wickTransform.localScale = localScale;
        }
    }
}
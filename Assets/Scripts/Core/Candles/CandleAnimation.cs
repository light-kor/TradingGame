using System.Collections;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace Core.Candles
{
    public static class CandleAnimation
    {
        public static IEnumerator AnimateCandle(this CandlePresenter presenter)
        {
            CandlePriceSettings priceSettings = presenter.PriceSettings;
            GameSettings settings = presenter.Settings;
            CandleProvider provider = presenter.Provider;

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

            float scaleMoveValue = firstTargetScale + firstTargetScale + thirdTargetScale +
                                   (thirdTargetScale - fourthTargetScale);
            float firstScaleTime = (firstTargetScale / scaleMoveValue) * settings.AnimationDuration;
            float secondScaleTime = firstScaleTime;
            float thirdScaleTime = (thirdTargetScale / scaleMoveValue) * settings.AnimationDuration;
            float fourthScaleTime =
                ((thirdTargetScale - fourthTargetScale) / scaleMoveValue) * settings.AnimationDuration;

            // Первая часть анимации: смена цвета и масштабирование вниз (или вверх) с фитилем.
            animSequence.AppendCallback(() => provider.SetColor(GetColorByDirection(settings, !isLong)));

            animSequence.Append(provider.CreateScaleTween(firstTargetScale, !isLong, firstScaleTime));
            animSequence.AppendCallback(() => provider.SetWickScaleSizeAndPosition(firstTargetScale, !isLong));
            animSequence.Append(provider.CreateScaleTween(secondTargetScale, !isLong, secondScaleTime));

            // Вторая часть анимации: смена цвета и масштабирование вверх (или вниз) с фитилем.
            animSequence.AppendCallback(() => provider.SetColor(GetColorByDirection(settings, !isLong)));

            animSequence.Append(provider.CreateScaleTween(thirdTargetScale, isLong, thirdScaleTime));
            animSequence.AppendCallback(() => provider.SetWickScaleSizeAndPosition(thirdTargetScale, isLong, firstTargetScale));
            animSequence.Append(provider.CreateScaleTween(fourthTargetScale, isLong, fourthScaleTime));

            animSequence.Play();

            yield return animSequence.WaitForCompletion();
        }

        private static Color GetColorByDirection(GameSettings settings, bool isLong)
        {
            return !isLong ? settings.LongColor : settings.ShortColor;
        }

        private static TweenerCore<Vector3, Vector3, VectorOptions> CreateScaleTween(this CandleProvider provider,
            float targetScale, bool isAboveZero, float duration)
        {
            return provider.BodyTransform.DOScaleY(targetScale, duration)
                .SetEase(Ease.Linear) //TODO: Выбрать лучший вариант
                .OnUpdate(() => UpdateBodyPosition(provider, isAboveZero));
        }

        private static void UpdateBodyPosition(CandleProvider provider, bool isAboveZero)
        {
            float currentScale = Mathf.Abs(provider.BodyTransform.localScale.y);
            float halfHeight = currentScale / 2f;
            Vector3 newLocalPos = provider.BodyTransform.localPosition;

            // Если анимация растёт вверх – центр устанавливается на halfHeight, иначе – -halfHeight.
            newLocalPos.y = isAboveZero ? halfHeight : -halfHeight;
            provider.BodyTransform.localPosition = newLocalPos;
        }

        private static void SetWickScaleSizeAndPosition(this CandleProvider provider, float targetScale,
            bool isAboveZero, float additionalScale = 0f)
        {
            float currentScale = targetScale + additionalScale;

            Transform wickTransform = provider.WickTransform;
            SetWickLocalPosition(isAboveZero, wickTransform, targetScale, additionalScale);
            SetWickScale(wickTransform, currentScale);
        }

        private static void SetWickLocalPosition(bool isAboveZero, Transform wickTransform, float targetScale, float additionalScale)
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

        private static void SetWickScale(Transform wickTransform, float currentScale)
        {
            Vector3 localScale = wickTransform.localScale;
            localScale.y = currentScale;
            wickTransform.localScale = localScale;
        }
    }
}
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Settings;
using UnityEngine;
using Zenject;

namespace Core.Candles.SpawnFacade
{
    public sealed class CandleSpawnAnimationFacade : CandleSpawnFacadeBase
    {
        [Inject] private readonly AnimationSettings _animationSettings;
        [Inject] private readonly CoreEventBus _coreEventBus;

        /// <summary>
        /// Анимация движения свечи с телом и хвостом
        /// </summary>
        public async UniTask AnimateCandleAsync(CandlePresenter presenter, CancellationToken ct = default)
        {
            ct.ThrowIfCancellationRequested();

            float animDuration = _animationSettings.CandleAnimationDuration;
            var priceSettings = presenter.PriceSettings;
            var provider = presenter.Provider;

            // Расчёт размеров сегментов анимации на основе настроек свечи.
            bool isLong = priceSettings.IsLong;
            float fourthTargetScale = Mathf.Abs(priceSettings.ClosePrice - priceSettings.OpenPrice);

            float firstTargetScale = isLong
                ? Mathf.Abs(priceSettings.LowPrice - priceSettings.OpenPrice)
                : Mathf.Abs(priceSettings.HighPrice - priceSettings.OpenPrice);

            float thirdTargetScale = isLong
                ? Mathf.Abs(priceSettings.HighPrice - priceSettings.OpenPrice)
                : Mathf.Abs(priceSettings.LowPrice - priceSettings.OpenPrice);

            float scaleMoveValue = firstTargetScale + firstTargetScale + thirdTargetScale + (thirdTargetScale - fourthTargetScale);
            float firstScaleTime = (firstTargetScale / scaleMoveValue) * animDuration;
            float secondScaleTime = firstScaleTime;
            float thirdScaleTime = (thirdTargetScale / scaleMoveValue) * animDuration;
            float fourthScaleTime = ((thirdTargetScale - fourthTargetScale) / scaleMoveValue) * animDuration;

            // --- собираем DOTween-последовательность ------------------------
            Sequence animSequence = DOTween.Sequence().SetLink(provider.BodyTransform.gameObject);

            // 1-я фаза: вниз/вверх + фитиль
            animSequence.AppendCallback(() => provider.SetColor(GetColorByDirection(isLong)));
            animSequence.Append(CreateScaleTween(presenter, firstTargetScale, !isLong, firstScaleTime));
            animSequence.AppendCallback(() => SetWickScaleSizeAndPosition(provider, firstTargetScale, !isLong));
            animSequence.Append(CreateScaleTween(presenter, 0f, !isLong, secondScaleTime));

            // 2-я фаза: обратное движение
            animSequence.AppendCallback(() => provider.SetColor(GetColorByDirection(isLong)));
            animSequence.Append(CreateScaleTween(presenter, thirdTargetScale, isLong, thirdScaleTime));
            animSequence.AppendCallback(() => SetWickScaleSizeAndPosition(provider, thirdTargetScale, isLong, firstTargetScale));
            animSequence.Append(CreateScaleTween(presenter, fourthTargetScale, isLong, fourthScaleTime));

            animSequence.Play();

            await animSequence.ToUniTask(cancellationToken: ct);
        }

        private Tween CreateScaleTween(CandlePresenter presenter, float targetScale, bool isAboveZero, float duration)
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
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
        [Inject] private readonly GameSettings _settings;

        /// <summary>
        /// Анимация движения свечи с телом и хвостом
        /// </summary>
        public async UniTask AnimateCandleAsync(CandlePresenter presenter)
        {
            var scaleData = GetCandleScaleData(presenter.PriceSettings);
            
            float animDuration = _animationSettings.CandleAnimationDuration;
            float scaleMoveValue = scaleData.FirstTarget + scaleData.FirstTarget + scaleData.ThirdTarget + 
                                   (scaleData.ThirdTarget - scaleData.FourthTarget);
            
            float firstScaleTime = (scaleData.FirstTarget / scaleMoveValue) * animDuration;
            float secondScaleTime = firstScaleTime;
            float thirdScaleTime = (scaleData.ThirdTarget / scaleMoveValue) * animDuration;
            float fourthScaleTime = ((scaleData.ThirdTarget - scaleData.FourthTarget) / scaleMoveValue) * animDuration;
            
            // --- собираем DOTween-последовательность ------------------------
            Sequence animSequence = DOTween.Sequence();
            bool isLong = presenter.PriceSettings.IsLong;
            var provider = presenter.Provider;

            // 1-я фаза: вниз/вверх + фитиль
            animSequence.AppendCallback(() => provider.SetColor(GetColorByDirection(!isLong)));
            animSequence.Append(CreateScaleTween(presenter, scaleData.FirstTarget, !isLong, firstScaleTime));
            animSequence.AppendCallback(() => SetWickScaleAndPosition(provider, scaleData.SecondTarget, !isLong));
            animSequence.Append(CreateScaleTween(presenter, 0f, !isLong, secondScaleTime));

            // 2-я фаза: обратное движение
            animSequence.AppendCallback(() => provider.SetColor(GetColorByDirection(isLong)));
            animSequence.Append(CreateScaleTween(presenter, scaleData.ThirdTarget, isLong, thirdScaleTime));
            animSequence.AppendCallback(() => SetWickScaleAndPosition(provider, scaleData.ThirdTarget, isLong, scaleData.FirstTarget));
            animSequence.Append(CreateScaleTween(presenter, scaleData.FourthTarget, isLong, fourthScaleTime));

            animSequence.Play();

            await animSequence.ToUniTask();
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
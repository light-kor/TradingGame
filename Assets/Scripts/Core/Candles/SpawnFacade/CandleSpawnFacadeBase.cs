using Settings;
using UnityEngine;
using Zenject;

namespace Core.Candles.SpawnFacade
{
    public abstract class CandleSpawnFacadeBase
    {
        [Inject] private readonly CurrentCoinFacade _currentCoinFacade;
        [Inject] private readonly GameSettings _settings;

        protected CandleScaleData GetCandleScaleData(CandlePriceSettings priceSettings)
        {
            bool isLong = priceSettings.IsLong;
            
            float rawFirstScale = isLong
                ? Mathf.Abs(priceSettings.LowPrice - priceSettings.OpenPrice)
                : Mathf.Abs(priceSettings.HighPrice - priceSettings.OpenPrice);

            float rawThirdScale = isLong
                ? Mathf.Abs(priceSettings.HighPrice - priceSettings.OpenPrice)
                : Mathf.Abs(priceSettings.LowPrice - priceSettings.OpenPrice);
            
            float rawFourthScale = Mathf.Abs(priceSettings.ClosePrice - priceSettings.OpenPrice);
            
            var coinPattern = _currentCoinFacade.GetCurrentPattern();
            float scaleMultiplier = coinPattern.VisualMultiplier;
            float firstTargetScale = rawFirstScale * scaleMultiplier;
            float secondTargetScale = firstTargetScale;
            float thirdTargetScale = rawThirdScale * scaleMultiplier;
            float fourthTargetScale = rawFourthScale * scaleMultiplier;
            
            var scaleData = new CandleScaleData(firstTargetScale, secondTargetScale, thirdTargetScale, fourthTargetScale);
            return scaleData;
        }
        
        protected Color GetColorByDirection(bool isLong)
        {
            return isLong ? _settings.LongColor : _settings.ShortColor;
        }
        
        protected void UpdateBodyPosition(CandleProvider provider, bool isAboveZero)
        {
            Transform bodyTransform = provider.BodyTransform;
            
            float currentScale = Mathf.Abs(bodyTransform.localScale.y);
            float halfHeight = currentScale / 2f;
            Vector3 newLocalPos = bodyTransform.localPosition;

            // Если анимация растёт вверх – центр устанавливается на halfHeight, иначе – -halfHeight.
            newLocalPos.y = isAboveZero ? halfHeight : -halfHeight;
            bodyTransform.localPosition = newLocalPos;
        }

        protected void SetWickScaleAndPosition(CandleProvider provider, float targetScale,
            bool isAboveZero, float additionalScale = 0f)
        {
            float currentScale = targetScale + additionalScale;

            Transform wickTransform = provider.WickTransform;
            SetWickLocalPosition(isAboveZero, wickTransform, targetScale, additionalScale);
            SetWickScale(wickTransform, currentScale);
        }
        
        private void SetWickLocalPosition(bool isAboveZero, Transform wickTransform, float targetScale, float additionalScale)
        {
            Vector3 localPos = wickTransform.localPosition;
            float halfHeight = (targetScale + additionalScale) / 2f;
            
            if (isAboveZero)
                localPos.y = halfHeight - additionalScale;
            else
                localPos.y = halfHeight - targetScale;
            
            wickTransform.localPosition = localPos;
        }

        private void SetWickScale(Transform wickTransform, float currentScale)
        {
            Vector3 localScale = wickTransform.localScale;
            localScale.y = currentScale;
            wickTransform.localScale = localScale;
        }
        
        protected virtual void UpdateCurrentPrice(CandlePresenter presenter, bool isAboveZero)
        {
            var coinPattern = _currentCoinFacade.GetCurrentPattern();
            
            float currentScaleY = presenter.Provider.BodyTransform.localScale.y;
            float currentScaleYWithSign = isAboveZero ? currentScaleY : -currentScaleY;
            float priceChange = currentScaleYWithSign / coinPattern.VisualMultiplier;
            
            presenter.UpdateCurrentPrice(priceChange);
        }
    }
}
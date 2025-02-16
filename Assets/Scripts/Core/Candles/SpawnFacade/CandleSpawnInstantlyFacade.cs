using UnityEngine;

namespace Core.Candles.SpawnFacade
{
    public class CandleSpawnInstantlyFacade : CandleSpawnFacadeBase
    {
        public void SpawnCandleInstantly(CandlePresenter presenter)
        {
            var priceSettings = presenter.PriceSettings;
            var provider = presenter.Provider;

            float firstTargetScale;
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

            provider.SetColor(GetColorByDirection(isLong));
            SetWickScaleSizeAndPosition(provider, thirdTargetScale, isLong, firstTargetScale);
            SetBodyScale(presenter, fourthTargetScale, isLong);
        }

        private void SetBodyScale(CandlePresenter presenter, float targetScale, bool isAboveZero)
        {
            var provider = presenter.Provider;
            var bodyScale = provider.BodyTransform.localScale;
            bodyScale.y = targetScale;
            provider.BodyTransform.localScale = bodyScale;
            
            UpdateCurrentPrice(presenter, isAboveZero);
            UpdateBodyPosition(provider, isAboveZero);
        }
    }
}
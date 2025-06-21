namespace Core.Candles.SpawnFacade
{
    public class CandleSpawnInstantlyFacade : CandleSpawnFacadeBase
    {
        public void SpawnCandleInstantly(CandlePresenter presenter)
        {
            var scaleData = GetCandleScaleData(presenter.PriceSettings);
            
            bool isLong = presenter.PriceSettings.IsLong;
            var provider = presenter.Provider;

            provider.SetColor(GetColorByDirection(isLong));
            SetWickScaleAndPosition(provider, scaleData.ThirdTarget, isLong, scaleData.FirstTarget);
            SetBodyScale(presenter, scaleData.FourthTarget, isLong);
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
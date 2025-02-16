using Settings;
using UnityEngine;

namespace Core.Candles
{
    public class CandlePresenter
    {
        private readonly GameSettings _gameSettings;
        public CandlePriceSettings PriceSettings { get; private set; }
        public CandleProvider Provider { get; private set; }
        public float CurrentPrice { get; private set; }
        
        public CandlePresenter(GameSettings settings, CandleProvider provider)
        {
            _gameSettings = settings;
            Provider = provider;
        }
        
        public void PrepareProvider()
        {
            Provider.SetActive(true);
            Provider.ResetCandleSize(_gameSettings.BodyWidth, _gameSettings.WickWidth);
        }
        
        public void SetPriceSettings(CandlePriceSettings priceSettings)
        {
            PriceSettings = priceSettings;
        }

        public void SetPosition(int xPos, float lastCurrentClosePrice)
        {
            Provider.SetPosition(new Vector3(xPos * _gameSettings.CandleSpawnOffset, lastCurrentClosePrice, 0));
        }
        
        public void UpdateCurrentPrice(float currentScale)
        {
            CurrentPrice = PriceSettings.OpenPrice + currentScale;
        }

        public Vector3 GetClosePricePosition()
        {
            var closePricePosition = Provider.transform.position;
            closePricePosition.y = CurrentPrice;
            return closePricePosition;
        }
    }
}
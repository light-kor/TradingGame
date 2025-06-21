using Settings;
using UnityEngine;

namespace Core.Candles
{
    public class CandlePresenter
    {
        private readonly GameSettings _gameSettings;
        public CandlePriceSettings PriceSettings { get; private set; }
        public CandleProvider Provider { get; }
        public float CurrentPrice { get; private set; }
        public Vector3 CurrentPricePosition{ get; private set; }
        
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
            Vector3 newPosition = new Vector3(xPos * _gameSettings.CandleSpawnOffset, lastCurrentClosePrice, 0);
            Provider.SetPosition(newPosition);
        }
        
        public void UpdateCurrentPrice(float priceChange)
        {
            CurrentPrice = PriceSettings.OpenPrice + priceChange;
            CurrentPricePosition = GetCurrentPricePosition(priceChange);
        }

        private Vector3 GetCurrentPricePosition(float priceChange)
        {
            var closePricePosition = Provider.transform.position;
            var bodySizeValue = priceChange * _gameSettings.VisualMultiplier;
            closePricePosition.y += bodySizeValue;
            
            return closePricePosition;
        }
    }
}
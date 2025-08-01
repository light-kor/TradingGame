using Settings;
using UnityEngine;
using Zenject;

namespace Core.Candles
{
    public class CandlePresenter
    {
        [Inject] private readonly CurrentCoinFacade _currentCoinFacade;
        [Inject] private readonly CandleProvider _candleProvider;
        [Inject] private readonly GameSettings _gameSettings;

        public CandlePriceSettings PriceSettings { get; private set; }
        public float CurrentPrice { get; private set; }
        public Vector3 CurrentPricePosition{ get; private set; }
        public CandleProvider Provider => _candleProvider;
        
        public void PrepareProvider()
        {
            _candleProvider.SetActive(true);
            _candleProvider.ResetCandleSize(_gameSettings.BodyWidth, _gameSettings.WickWidth);
        }
        
        public void SetPriceSettings(CandlePriceSettings priceSettings)
        {
            PriceSettings = priceSettings;
        }

        public void SetPosition(int xPos, float lastCurrentClosePrice)
        {
            Vector3 newPosition = new Vector3(xPos * _gameSettings.CandleSpawnOffset, lastCurrentClosePrice, 0);
            _candleProvider.SetPosition(newPosition);
        }
        
        public void UpdateCurrentPrice(float priceChange)
        {
            CurrentPrice = PriceSettings.OpenPrice + priceChange;
            CurrentPricePosition = GetCurrentPricePosition(priceChange);
        }

        private Vector3 GetCurrentPricePosition(float priceChange)
        {
            var coinPattern = _currentCoinFacade.GetCurrentPattern();
            
            var closePricePosition = _candleProvider.transform.position;
            var bodySizeValue = priceChange * coinPattern.VisualMultiplier;
            closePricePosition.y += bodySizeValue;
            
            return closePricePosition;
        }
    }
}
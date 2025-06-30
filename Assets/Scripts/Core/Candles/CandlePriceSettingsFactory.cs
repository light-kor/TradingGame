using Settings;
using UnityEngine;
using Zenject;

namespace Core.Candles
{
    public class CandlePriceSettingsFactory
    {
        [Inject] private readonly CurrentCoinFacade _currentCoinFacade;
        private PriceMovePatternSettings _currentPattern;
        
        public CandlePriceSettings CreateCandlePriceSettings(float openPrice)
        {
            _currentPattern = _currentCoinFacade.GetCurrentPattern();
            float closePrice, highPrice, lowPrice;
            bool isLong = GetRandomSign();
            

            if (isLong)
            {
                closePrice = openPrice + GetBodyRandomChange(openPrice);
                highPrice = closePrice + GetWickRandomChange(closePrice);
                lowPrice = openPrice - GetWickRandomChange(openPrice);
            }
            else
            {
                closePrice = openPrice - GetBodyRandomChange(openPrice);
                highPrice = openPrice + GetWickRandomChange(openPrice);
                lowPrice = closePrice - GetWickRandomChange(closePrice);
            }
            
            return new CandlePriceSettings(openPrice, closePrice, highPrice, lowPrice, isLong);
        }
        
        private float GetBodyRandomChange(float currentPrice)
        {
            float randomBodyPercent = Random.Range(_currentPattern.MinBodyPercent, _currentPattern.MaxBodyPercent);
            float bodySize = currentPrice * randomBodyPercent / 100f;
            return bodySize;
        }

        private float GetWickRandomChange(float currentPrice)
        {
            float randomWickPercent = Random.Range(0, _currentPattern.MaxWickPercent);
            float wickSize = currentPrice * randomWickPercent / 100f;
            return wickSize;
        }
        
        private bool GetRandomSign()
        {
            return Random.Range(0, 2) != 0;
        }
    }
}
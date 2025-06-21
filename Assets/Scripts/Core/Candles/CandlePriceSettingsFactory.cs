using Settings;
using UnityEngine;
using Zenject;

namespace Core.Candles
{
    public class CandlePriceSettingsFactory
    {
        [Inject] private readonly GameSettings _settings;
        
        public CandlePriceSettings CreateCandlePriceSettings(float openPrice)
        {
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
            float randomBodyPercent = Random.Range(_settings.MinBodyPercent, _settings.MaxBodyPercent);
            float bodySize = currentPrice * randomBodyPercent / 100f;
            return bodySize;
        }

        private float GetWickRandomChange(float currentPrice)
        {
            float randomWickPercent = Random.Range(0, _settings.MaxWickPercent);
            float wickSize = currentPrice * randomWickPercent / 100f;
            return wickSize;
        }
        
        private bool GetRandomSign()
        {
            return Random.Range(0, 2) != 0;
        }
    }
}
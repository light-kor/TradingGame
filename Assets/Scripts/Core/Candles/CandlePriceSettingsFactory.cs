using UnityEngine;
using Zenject;

namespace Core.Candles
{
    public class CandlePriceSettingsFactory
    {
        [Inject] private readonly GameSettings _settings;
        
        public CandlePriceSettings CreateCandlePriceSettings(float openPrice)
        {
            bool isLong = GetRandomSign();
            float closePrice;
            float highPrice;
            float lowPrice;

            if (isLong)
            {
                closePrice = openPrice + GetBodyRandomValue();
                highPrice = closePrice + GetWickRandomValue();
                lowPrice = openPrice - GetWickRandomValue();
            }
            else
            {
                closePrice = openPrice - GetBodyRandomValue();
                highPrice = openPrice + GetWickRandomValue();
                lowPrice = closePrice - GetWickRandomValue();
            }
            
            return new CandlePriceSettings(openPrice, closePrice, highPrice, lowPrice, isLong);
        }
        
        private float GetBodyRandomValue()
        {
            return Random.Range(_settings.MinBodySize, _settings.MaxBodySize);
        }

        private float GetWickRandomValue()
        {
            return Random.Range(0f, _settings.MaxWickSize);
        }
        
        private bool GetRandomSign()
        {
            return Random.Range(0, 2) != 0;
        }
    }
}
using UnityEngine;
using Zenject;

namespace Core.Candles
{
    public class CandleFactory
    {
        [Inject] private GameSettings _settings;

        public CandleProvider SpawnCandle(float openPrice)
        {
            CandleProvider newCandle = Object.Instantiate(_settings.CandlePrefab);
            var candlePriceSettings = CreateCandlePriceSettings(openPrice);
            newCandle.InitCandle(candlePriceSettings, _settings);
            return newCandle;
        }
        
        private CandlePriceSettings CreateCandlePriceSettings(float openPrice)
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
            return Random.Range(_settings.MinBodyValue, _settings.MaxBodyValue);
        }

        private float GetWickRandomValue()
        {
            return Random.Range(0f, _settings.MaxWickValue);
        }
        
        private bool GetRandomSign()
        {
            return Random.Range(0, 2) != 0;
        }
    }
}
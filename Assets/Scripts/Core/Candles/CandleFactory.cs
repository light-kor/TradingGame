using UnityEngine;
using Zenject;

namespace Core.Candles
{
    public class CandleFactory
    {
        [Inject] private GameSettings _settings;

        public CandleProvider SpawnCandle()
        {
            CandleProvider newCandle = Object.Instantiate(_settings.CandlePrefab);
            var candlePriceSettings = CreateCandlePriceSettings();
            newCandle.InitCandle(candlePriceSettings, _settings);
            return newCandle;
        }
        
        private CandlePriceSettings CreateCandlePriceSettings()
        {
            bool isLong = GetRandomSign();
            float closePrice;
            float highPrice;
            float lowPrice;

            if (isLong)
            {
                closePrice = Random.Range(_settings.MinBodyValue, _settings.MaxBodyValue);
                highPrice = closePrice + GetWickRandomValue();
                lowPrice = -GetWickRandomValue();
            }
            else
            {
                closePrice = -Random.Range(_settings.MinBodyValue, _settings.MaxBodyValue);
                highPrice = GetWickRandomValue();
                lowPrice = closePrice - GetWickRandomValue();
            }
            
            return new CandlePriceSettings(closePrice, highPrice, lowPrice, isLong);
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
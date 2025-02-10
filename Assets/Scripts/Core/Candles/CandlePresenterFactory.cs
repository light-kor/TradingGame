using UnityEngine;
using Zenject;

namespace Core.Candles
{
    public class CandlePresenterFactory
    {
        [Inject] private GameSettings _settings;

        public CandlePresenter CreateCandle(float openPrice)
        {
            var candlePriceSettings = CreateCandlePriceSettings(openPrice);
            CandlePresenter newCandle = new CandlePresenter(candlePriceSettings, _settings);
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
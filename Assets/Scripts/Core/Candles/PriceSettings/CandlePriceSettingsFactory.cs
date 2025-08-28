using Core.TradePosition;
using Settings;
using UnityEngine;
using Zenject;

namespace Core.Candles.PriceSettings
{
    public class CandlePriceSettingsFactory
    {
        [Inject] private readonly CurrentPositionController _currentPositionController;
        [Inject] private readonly CurrentCoinFacade _currentCoinFacade;
        [Inject] private readonly RandomUtils _randomUtils;
        private PriceMovePatternSettings _currentPattern;
        private float _currentBasePrice;
        
        public CandlePriceSettings CreateCandlePriceSettings(float openPrice)
        {
            _currentPattern = _currentCoinFacade.GetCurrentPattern();
            _currentBasePrice = GetBasePrice(openPrice);
            
            float closePrice, highPrice, lowPrice;
            bool isLong = _randomUtils.IsLong();
            
            if (isLong)
            {
                closePrice = openPrice + GetBodyRandomChange();
                highPrice = closePrice + GetWickRandomChange();
                lowPrice = openPrice - GetWickRandomChange();
            }
            else
            {
                closePrice = openPrice - GetBodyRandomChange();
                highPrice = openPrice + GetWickRandomChange();
                lowPrice = closePrice - GetWickRandomChange();
                
                closePrice = Mathf.Max(closePrice, 0f);
                highPrice = Mathf.Max(highPrice, 0f);
                lowPrice = Mathf.Max(lowPrice, 0f);
            }
            
            return new CandlePriceSettings(openPrice, closePrice, highPrice, lowPrice, isLong);
        }
        
        private float GetBasePrice(float currentPrice)
        {
            float startPrice = _currentPositionController.IsOpen
                ? _currentPositionController.EntryPrice
                : _currentPattern.InitialPrice;
            
            if (currentPrice < startPrice)
                return startPrice;

            float averagePrice = (currentPrice + startPrice) / 2f;
            return averagePrice;
        }

        private float GetBodyRandomChange()
        {
            float randomBodyPercent = Random.Range(_currentPattern.MinBodyPercent, _currentPattern.MaxBodyPercent);
            float bodySize = _currentBasePrice * randomBodyPercent / 100f;
            return bodySize;
        }

        private float GetWickRandomChange()
        {
            float randomWickPercent = Random.Range(0, _currentPattern.MaxWickPercent);
            float wickSize = _currentBasePrice * randomWickPercent / 100f;
            return wickSize;
        }
    }
}
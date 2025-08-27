using Core.TradePosition;
using Zenject;

namespace Core.Candles.SpawnFacade
{
    public class CandleVisualMultiplierApplier
    {
        [Inject] private readonly CurrentPositionController _currentPositionController;
        [Inject] private readonly CandleSequenceController _candleSequenceController;
        [Inject] private readonly CurrentCoinFacade _currentCoinFacade;
        
        /// <summary>
        /// Метод создан для визуального уменьшения свечей, если они цена открытия выше начальной цены 
        /// </summary>
        public float GetVisualMultiplier()
        {
            var coinPattern = _currentCoinFacade.GetCurrentPattern();
            var currentCandle = _candleSequenceController.CurrentCandlePresenter;
            
            float startPrice = _currentPositionController.IsOpen
                ? _currentPositionController.EntryPrice
                : coinPattern.InitialPrice;
            
            if (currentCandle.PriceSettings.OpenPrice < startPrice)
                return coinPattern.VisualMultiplier;

            float priceChangeValue = startPrice / currentCandle.PriceSettings.OpenPrice;
            float finalMultiplier = coinPattern.VisualMultiplier * priceChangeValue;
            
            return finalMultiplier;
        }
    }
}
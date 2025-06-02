using System;
using Core.Candles;
using Core.UI.Common;
using Core.UI.Providers;
using Zenject;

namespace Core.UI.Presenters
{
    public class CoinPricePresenter : IInitializable, IDisposable
    {
        [Inject] private readonly CoreMainPanelProvider _mainPanelProvider;
        [Inject] private readonly CoreEventBus _coreEventBus;
        
        private CurrencyProvider _coinPriceProvider;
        
        public float CurrentPrice { get; private set; }
        
        public void Initialize()
        {
            _coinPriceProvider = _mainPanelProvider.CoinPriceProvider;
            
            _coreEventBus.OnCurrentPriceUpdated += UpdateCurrentPrice;
        }

        public void Dispose()
        {
            _coreEventBus.OnCurrentPriceUpdated -= UpdateCurrentPrice;
        }
        
        private void UpdateCurrentPrice(CandlePresenter currentCandle)
        {
            CurrentPrice = currentCandle.CurrentPrice;
            _coinPriceProvider.SetValue(CurrentPrice);
        }
    }
}
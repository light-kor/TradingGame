using System;
using Core.Candles;
using Core.UI.Providers;
using Zenject;

namespace Core.UI.Presenters
{
    public class PriceLinePresenter : IInitializable, IDisposable
    {
        [Inject] private readonly CandleSequenceController _candleSequenceController;
        [Inject] private readonly CoreMainPanelProvider _mainPanelProvider;
        [Inject] private readonly CoreEventBus _coreEventBus;
        private CandlePresenter _lastCandlePresenter;
        private PriceLineProvider _priceLineProvider;
        
        public void Initialize()
        {
            _priceLineProvider = _mainPanelProvider.PriceLineProvider;
            
            _coreEventBus.OnCandleSpawned += UpdateLastCandleData;
            _coreEventBus.OnCameraMoved += UpdatePriceLineByCameraMoved;
        }

        public void Dispose()
        {
            _coreEventBus.OnCandleSpawned -= UpdateLastCandleData;
            _coreEventBus.OnCameraMoved -= UpdatePriceLineByCameraMoved;
        }

        private void UpdateLastCandleData(CandlePresenter currentCandle)
        {
            _lastCandlePresenter = currentCandle;
            UpdateLinePosition();
        }
        
        private void UpdateLinePosition()
        {
            if (_lastCandlePresenter == null)
                return;

            _priceLineProvider.UpdateLinePosition(_lastCandlePresenter);
        }
        
        private void UpdatePriceLineByCameraMoved()
        {
            if (_candleSequenceController.IsSpawning)
                return;
            
            UpdateLinePosition();
        }
    }
}
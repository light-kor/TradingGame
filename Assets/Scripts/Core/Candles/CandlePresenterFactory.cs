using System.Collections.Generic;
using Core.Pool;
using Settings;
using Zenject;

namespace Core.Candles
{
    public class CandlePresenterFactory
    {
        [Inject] private readonly CandleProviderPool _candleProviderPool;
        [Inject] private readonly GameSettings _settings;

        private readonly Queue<CandlePresenter> _candlesPoll = new();
        
        public CandlePresenter GetFreeCandlePresenter()
        {
            if (_candlesPoll.Count < _settings.CandlesPoolCount)
                return CreateCandlePresenter();
            
            return GetOldestCandlePresenter();
        }
        
        private CandlePresenter CreateCandlePresenter()
        {
            var candleProvider = _candleProviderPool.GetFreeProvider();
            CandlePresenter newCandle = new CandlePresenter(_settings, candleProvider);
            _candlesPoll.Enqueue(newCandle);
            return newCandle;
        }
        
        private CandlePresenter GetOldestCandlePresenter()
        {
            var candle = _candlesPoll.Dequeue();
            _candlesPoll.Enqueue(candle);
            return candle;
        }
    }
}
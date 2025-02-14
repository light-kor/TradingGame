using System;
using System.Collections;
using System.Collections.Generic;
using Core.Candles;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Core.Pool
{
    public class CandleProviderPool
    {
        [Inject] private readonly CandleProvidersContainer _candleProvidersContainer;
        [Inject] private readonly GameSettings _settings;

        private Queue<CandleProvider> _candlesPoll;

        public IEnumerator InitializePool(Action callback)
        {
            var candleInstantiateOperation = Object.InstantiateAsync(_settings.CandlePrefab, _settings.CandlesPoolCount,
                _candleProvidersContainer.Transform);

            while (!candleInstantiateOperation.isDone)
                yield return null;

            _candlesPoll = new Queue<CandleProvider>();

            foreach (var candle in candleInstantiateOperation.Result)
                StoreProvider(candle);

            callback?.Invoke();
        }

        private void StoreProvider(CandleProvider candle)
        {
            candle.SetActive(false);
            _candlesPoll.Enqueue(candle);
        }
        
        public void ReleaseInstance(CandleProvider candle)
        {
            StoreProvider(candle);
        }

        public CandleProvider GetFreeProvider()
        {
            if (_candlesPoll.TryDequeue(out var result))
                return result;

            Debug.LogError("CandleProviders Pool Dequeue Error");
            return SpawnSingleCandleProvider();
        }

        private CandleProvider SpawnSingleCandleProvider()
        {
            return Object.Instantiate(_settings.CandlePrefab, _candleProvidersContainer.Transform);
        }
    }
}
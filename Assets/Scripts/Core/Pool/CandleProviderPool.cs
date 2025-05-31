using System.Collections.Generic;
using System.Threading;
using Core.Candles;
using Cysharp.Threading.Tasks;
using Settings;
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

        public async UniTask InitializePoolAsync(CancellationToken ct = default)
        {
            var handle = Object.InstantiateAsync(
                _settings.CandlePrefab,
                _settings.CandlesPoolCount,
                _candleProvidersContainer.Transform);

            await handle.ToUniTask(cancellationToken: ct);

            _candlesPoll = new Queue<CandleProvider>(handle.Result.Length);

            foreach (var candle in handle.Result)
                StoreProvider(candle);
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
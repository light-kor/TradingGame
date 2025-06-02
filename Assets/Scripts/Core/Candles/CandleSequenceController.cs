using System.Threading;
using Core.Candles.SpawnFacade;
using Core.Pool;
using Cysharp.Threading.Tasks;
using Settings;
using UnityEngine;
using Zenject;

namespace Core.Candles
{
    public class CandleSequenceController : IInitializable
    {
        [Inject] private readonly CandlePriceSettingsFactory _candlePriceSettingsFactory;
        [Inject] private readonly CandleSpawnAnimationFacade _candleSpawnAnimationFacade;
        [Inject] private readonly CandleSpawnInstantlyFacade _candleSpawnInstantlyFacade;
        [Inject] private readonly CandlePresenterFactory _candlePresenterFactory;
        [Inject] private readonly CameraMoveController _cameraMoveController;
        [Inject] private readonly CandleProviderPool _candleProviderPool;
        [Inject] private readonly CoreEventBus _coreEventBus;
        [Inject] private readonly GameSettings _settings;
        
        private CancellationTokenSource _spawnCandlesCts;
        private float _currentClosePrice;
        private int _currentXPosition;
        
        public bool IsSpawning { get; private set; }
        public CandlePresenter LastCandlePresenter { get; private set; }
        public Vector3 LastCandleClosePosition => LastCandlePresenter.GetClosePricePosition();
        public float CurrentPrice => LastCandlePresenter.CurrentPrice;

        public void Initialize()
        {
            InitializeCandles();
        }
        
        private async void InitializeCandles()
        {
            await _candleProviderPool.InitializePoolAsync();
            SpawnCandleSequenceInstantly(_settings.CandlesPoolCount);
        }
        
        public async void StartSpawnCandles()
        {
            if (IsSpawning)
            {
                StopSpawn();
                await WaitUntilSpawningFinishedAsync();
            }
            
            IsSpawning = true;
            _spawnCandlesCts = new CancellationTokenSource();
            SpawnCandleInfinite(_spawnCandlesCts.Token);
        }
        
        
        private async void SpawnCandleInfinite(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var candle = CreateNewCandle();
                await _candleSpawnAnimationFacade.AnimateCandleAsync(candle);
            
                if (token.IsCancellationRequested) 
                    break;
            
                _cameraMoveController.MoveCameraWithAnimation(LastCandleClosePosition);
            }
            
            IsSpawning = false;
            _spawnCandlesCts?.Dispose();
            _spawnCandlesCts = null;
        }

        public void StopSpawn()
        {
            _spawnCandlesCts?.Cancel();
        }
        
        public UniTask WaitUntilSpawningFinishedAsync()
        {
            return UniTask.WaitUntil(() => !IsSpawning);
        }
        
        private void SpawnCandleSequenceInstantly(int candleCount)
        {
            for (int i = 0; i < candleCount; i++)
            {
                var candle = CreateNewCandle();
                _candleSpawnInstantlyFacade.SpawnCandleInstantly(candle);
            }

            _cameraMoveController.MoveCameraInstantly(LastCandleClosePosition);
            _coreEventBus.FireCurrentPriceUpdated(LastCandlePresenter);
        }

        private CandlePresenter CreateNewCandle()
        {
            var candle = _candlePresenterFactory.GetFreeCandlePresenter();
            candle.PrepareProvider();

            var candlePriceSettings = _candlePriceSettingsFactory.CreateCandlePriceSettings(_currentClosePrice);
            candle.SetPriceSettings(candlePriceSettings);
            candle.SetPosition(_currentXPosition, _currentClosePrice);

            LastCandlePresenter = candle;
            _currentClosePrice = candle.PriceSettings.ClosePrice;
            _currentXPosition++;
            return candle;
        }
    }
}
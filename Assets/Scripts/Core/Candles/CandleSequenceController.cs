using System.Threading;
using Core.Candles.PriceSettings;
using Core.Candles.SpawnFacade;
using Cysharp.Threading.Tasks;
using Settings;
using UnityEngine;
using Zenject;

namespace Core.Candles
{
    public class CandleSequenceController
    {
        [Inject] private readonly CandleSpawnAnimationFacade _candleSpawnAnimationFacade;
        [Inject] private readonly CandleSpawnInstantlyFacade _candleSpawnInstantlyFacade;
        [Inject] private readonly CandlePriceSettingsFacade _candlePriceSettingsFacade;
        [Inject] private readonly CandlePresenterFactory _candlePresenterFactory;
        [Inject] private readonly CameraMoveController _cameraMoveController;
        [Inject] private readonly CoreEventBus _coreEventBus;
        [Inject] private readonly GameSettings _settings;

        private CancellationTokenSource _spawnCandlesCts;
        private float _currentClosePrice;
        private int _currentXPosition;

        private Vector3 LastClosePosition => CurrentCandlePresenter?.CurrentPricePosition ?? Vector3.zero;
        public bool IsSpawning { get; private set; }
        public CandlePresenter CurrentCandlePresenter { get; private set; }

        public void InitializeCoin(float initialPrice)
        {
            _currentXPosition = 0;
            CurrentCandlePresenter = null;

            _currentClosePrice = initialPrice;
            SpawnCandleSequenceInstantly(_settings.CandlesPoolCount);
        }

        public async void StartSpawnCandles()
        {
            if (IsSpawning)
            {
                StopSpawn();
                await UniTask.WaitUntil(() => !IsSpawning);
            }

            IsSpawning = true;
            _spawnCandlesCts = new CancellationTokenSource();
            SpawnCandleInfinite(_spawnCandlesCts.Token);
        }


        private async void SpawnCandleInfinite(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var candle = CreateNewCandle(false);
                await _candleSpawnAnimationFacade.AnimateCandleAsync(candle);

                if (token.IsCancellationRequested)
                    break;

                _cameraMoveController.MoveCameraWithAnimation(LastClosePosition);
            }

            IsSpawning = false;
            _spawnCandlesCts?.Dispose();
            _spawnCandlesCts = null;
        }

        public void StopSpawn()
        {
            _spawnCandlesCts?.Cancel();
        }

        private async void SpawnCandleSequenceInstantly(int candleCount)
        {
            for (int i = 0; i < candleCount; i++)
            {
                var candle = CreateNewCandle(true);
                _candleSpawnInstantlyFacade.SpawnCandleInstantly(candle);
            }

            await UniTask.Yield(PlayerLoopTiming.Update);
            _cameraMoveController.MoveCameraInstantly(LastClosePosition);
            await UniTask.Yield(PlayerLoopTiming.Update);
            _coreEventBus.FireCurrentPriceUpdated(CurrentCandlePresenter);
        }

        private CandlePresenter CreateNewCandle(bool isInstantlySpawn)
        {
            var candle = _candlePresenterFactory.GetFreeCandlePresenter();
            candle.PrepareProvider();

            var candlePriceSettings = isInstantlySpawn 
                ? _candlePriceSettingsFacade.CreateDefaultCandlePriceSettings(_currentClosePrice) 
                : _candlePriceSettingsFacade.CreateRandomCandlePriceSettings(_currentClosePrice);
            
            candle.SetPriceSettings(candlePriceSettings);
            candle.SetPosition(_currentXPosition, LastClosePosition.y);

            CurrentCandlePresenter = candle;
            _currentClosePrice = candle.PriceSettings.ClosePrice;
            _currentXPosition++;
            return candle;
        }
    }
}
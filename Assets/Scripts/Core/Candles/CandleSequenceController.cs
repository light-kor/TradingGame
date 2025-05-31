using System;
using Core.Candles.SpawnFacade;
using Core.Pool;
using Core.UI.Providers;
using Settings;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace Core.Candles
{
    public class CandleSequenceController : IInitializable, IDisposable
    {
        [Inject] private readonly CandlePriceSettingsFactory _candlePriceSettingsFactory;
        [Inject] private readonly CandleSpawnAnimationFacade _candleSpawnAnimationFacade;
        [Inject] private readonly CandleSpawnInstantlyFacade _candleSpawnInstantlyFacade;
        [Inject] private readonly CandlePresenterFactory _candlePresenterFactory;
        [Inject] private readonly CameraMoveController _cameraMoveController;
        [Inject] private readonly CoreMainPanelProvider _mainPanelProvider;
        [Inject] private readonly CandleProviderPool _candleProviderPool;
        [Inject] private readonly CoreEventBus _coreEventBus;
        [Inject] private readonly GameSettings _settings;
        
        private float _currentClosePrice;
        private int _currentXPosition;
        
        public CandlePresenter LastCandlePresenter { get; private set; }
        public bool SpawnInProcess { get; private set; }
        public Vector3 LastCandleClosePosition => LastCandlePresenter.GetClosePricePosition();

        public void Initialize()
        {
            Assert.IsTrue(_settings.CandlesSpawnCount > 0);
            InitializeCandles();

            _mainPanelProvider.ContinueButton.OnButtonClicked += SpawnCandles;
        }
        
        public void Dispose()
        {
            _mainPanelProvider.ContinueButton.OnButtonClicked -= SpawnCandles;
        }
        
        private async void InitializeCandles()
        {
            await _candleProviderPool.InitializePoolAsync();
            SpawnCandleSequenceInstantly(_settings.CandlesSpawnCount);
        }
        
        private void SpawnCandles()
        {
            SpawnCandleSequence(_settings.CandlesSpawnCount);
        }
        
        private async void SpawnCandleSequence(int candleCount)
        {
            //TODO: Надо ли в SpawnCandleSequenceInstantly?
            SpawnInProcess = true;
            
            for (int i = 0; i < candleCount; i++)
            {
                var candle = CreateNewCandle();
                await _candleSpawnAnimationFacade.AnimateCandleAsync(candle);

                _cameraMoveController.MoveCameraWithAnimation(LastCandleClosePosition);
            }

            SpawnInProcess = false;
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
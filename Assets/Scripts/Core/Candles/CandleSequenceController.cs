using System.Collections;
using Core.Candles.SpawnFacade;
using Core.Pool;
using Core.UI;
using Settings;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace Core.Candles
{
    public class CandleSequenceController : MonoBehaviour
    {
        [SerializeField] 
        private ButtonProvider continueButton;
        
        [Inject] private readonly CandlePriceSettingsFactory _candlePriceSettingsFactory;
        [Inject] private readonly CandlePresenterFactory _candlePresenterFactory;
        [Inject] private readonly CandleSpawnAnimationFacade _candleSpawnAnimationFacade;
        [Inject] private readonly CandleSpawnInstantlyFacade _candleSpawnInstantlyFacade;
        [Inject] private readonly CameraMoveController _cameraMoveController;
        [Inject] private readonly CandleProviderPool _candleProviderPool;
        [Inject] private readonly GameSettings _settings;
        [Inject] private readonly CoreEventBus _coreEventBus;
        
        private float _currentClosePrice;
        private int _currentXPosition;
        
        public CandlePresenter LastCandlePresenter { get; private set; }
        public bool SpawnInProcess { get; private set; }
        public Vector3 LastCandleClosePosition => LastCandlePresenter.GetClosePricePosition();

        private void Start()
        {
            Assert.IsTrue(_settings.CandlesSpawnCount > 0);
            
            continueButton.OnButtonClicked += SpawnCandles;

            StartCoroutine(_candleProviderPool.InitializePool(() =>
            {
                SpawnCandleSequenceInstantly(_settings.CandlesSpawnCount);
            }));
        }

        private void OnDestroy()
        {
            continueButton.OnButtonClicked -= SpawnCandles;
        }
        
        private void SpawnCandles()
        {
            StartCoroutine(SpawnCandleSequence(_settings.CandlesSpawnCount));
        }

        private IEnumerator SpawnCandleSequence(int candleCount)
        {
            //TODO: Надо ли в SpawnCandleSequenceInstantly?
            SpawnInProcess = true;
            
            for (int i = 0; i < candleCount; i++)
            {
                var candle = CreateNewCandle();
                yield return _candleSpawnAnimationFacade.AnimateCandle(candle);

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
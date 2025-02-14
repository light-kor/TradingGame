using System.Collections;
using Core.Pool;
using Core.UI;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace Core.Candles
{
    public class CandleSequenceController : MonoBehaviour
    {
        private const int START_X_POSITION = -16;
        
        [Inject] private readonly CandlePriceSettingsFactory _candlePriceSettingsFactory;
        [Inject] private readonly CandlePresenterFactory _candlePresenterFactory;
        [Inject] private readonly ContinueButtonProvider _continueButtonProvider;
        [Inject] private readonly CandleAnimationFacade _candleAnimationFacade;
        [Inject] private readonly CandleProviderPool _candleProviderPool;
        [Inject] private readonly GameSettings _settings;

        private float _currentClosePrice;
        private int _currentXPosition;

        private void Start()
        {
            Assert.IsTrue(_settings.CandlesSpawnCount > 0);
            
            _continueButtonProvider.OnButtonClicked += SpawnCandles;

            _currentXPosition = START_X_POSITION;
            _currentClosePrice = 0;
            
            StartCoroutine(_candleProviderPool.InitializePool(() =>
            {
                StartCoroutine(SpawnCandleSequence(_settings.CandlesSpawnCount, true));
            }));
        }
        
        private void OnDestroy()
        {
            _continueButtonProvider.OnButtonClicked -= SpawnCandles;
        }
        
        private void SpawnCandles()
        {
            StartCoroutine(SpawnCandleSequence(_settings.CandlesSpawnCount));
        }

        private IEnumerator SpawnCandleSequence(int candleCount, bool instantlySpawn = false)
        {
            for (int i = 0; i < candleCount; i++)
            {
                var candle = _candlePresenterFactory.GetFreeCandlePresenter();
                candle.PrepareProvider();
                
                var candlePriceSettings = _candlePriceSettingsFactory.CreateCandlePriceSettings(_currentClosePrice);
                candle.SetPriceSettings(candlePriceSettings);
                candle.SetPosition(_currentXPosition, _currentClosePrice);
                
                yield return _candleAnimationFacade.AnimateCandle(candle, instantlySpawn);
                
                _currentClosePrice = candle.PriceSettings.ClosePrice;
                _currentXPosition++;
            }
        }
    }
}
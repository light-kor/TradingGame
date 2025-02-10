using System.Collections;
using UnityEngine;
using Zenject;

namespace Core.Candles
{
    public class CandleSequenceController : MonoBehaviour
    {
        [Inject] private CandlePresenterFactory _candlePresenterFactory;
        [Inject] private GameSettings _settings;

        private float _currentClosePrice;

        private void Start()
        {
            _currentClosePrice = 0;
            StartCoroutine(SpawnCandleSequence(20));
        }

        private IEnumerator SpawnCandleSequence(int candleCount)
        {
            for (int i = 0; i < candleCount; i++)
            {
                var candle = _candlePresenterFactory.CreateCandle(_currentClosePrice);
                var candleProvider = GetFreeCandleProvider();
                
                candle.SetProvider(candleProvider);
                candle.SetPosition(i, _currentClosePrice);
                
                yield return CandleAnimation.AnimateCandle(candle);
                
                _currentClosePrice = candle.PriceSettings.ClosePrice;
            }
        }

        private CandleProvider GetFreeCandleProvider()
        {
            CandleProvider newCandle = Object.Instantiate(_settings.CandlePrefab);
            newCandle.ResetCandleSize(_settings);
            return newCandle;
        }
    }
}
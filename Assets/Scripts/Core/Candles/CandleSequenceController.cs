using System.Collections;
using UnityEngine;
using Zenject;

namespace Core.Candles
{
    public class CandleSequenceController : MonoBehaviour
    {
        [Inject] private CandleFactory _candleFactory;
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
                var candle = _candleFactory.SpawnCandle();
                
                candle.SetPosition(i, _currentClosePrice);
                candle.AnimateCandle();
                _currentClosePrice = candle.GetClosePosition();

                yield return new WaitForSeconds(_settings.AnimationDuration);
            }
        }
    }
}

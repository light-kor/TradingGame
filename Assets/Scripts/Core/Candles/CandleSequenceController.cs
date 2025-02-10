using System.Collections;
using UnityEngine;
using Zenject;

namespace Core.Candles
{
    public class CandleSequenceController : MonoBehaviour
    {
        [Inject] private CandleFactory _candleFactory;

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
                var candle = _candleFactory.SpawnCandle(_currentClosePrice);
                
                candle.SetPosition(i, _currentClosePrice);
                yield return candle.AnimateCandle();
                
                _currentClosePrice = candle.ClosePrice;
            }
        }
    }
}
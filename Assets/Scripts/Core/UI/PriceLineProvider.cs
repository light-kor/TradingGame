using Core.Candles;
using UnityEngine;
using Zenject;

namespace Core.UI
{
    public class PriceLineProvider : MonoBehaviour
    {
        [SerializeField] 
        private CandleSequenceController candleSequenceController;

        [Inject] private readonly CoreEventBus _coreEventBus;
        
        private CandlePresenter _lastCandlePresenter;

        private void Awake()
        {
            _coreEventBus.OnCurrentPriceUpdated += UpdateLastCandleData;
            _coreEventBus.OnNeedUpdatePriceLineByCameraMove += UpdatePriceLineByCameraMove;
        }

        private void OnDestroy()
        {
            _coreEventBus.OnCurrentPriceUpdated -= UpdateLastCandleData;
            _coreEventBus.OnNeedUpdatePriceLineByCameraMove -= UpdatePriceLineByCameraMove;
        }

        private void UpdateLastCandleData(CandlePresenter currentCandle)
        {
            _lastCandlePresenter = currentCandle;
            UpdateLinePosition();
        }
        
        private void UpdateLinePosition()
        {
            if (_lastCandlePresenter == null)
                return;
            
            var currentPrice = _lastCandlePresenter.CurrentPrice;
            
            var linePosition = transform.position;
            linePosition.y = currentPrice;
            transform.position = linePosition;
        }
        
        private void UpdatePriceLineByCameraMove()
        {
            if (candleSequenceController.SpawnInProcess)
                return;
            
            UpdateLinePosition();
        }
    }
}
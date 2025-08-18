using System;
using Core.Candles;
using Core.TradePosition.Close;
using Core.UI.Providers;
using UnityEngine;
using Zenject;

namespace Core.UI.Presenters
{
    public class EnterPriceLinePresenter : IInitializable, IDisposable
    {
        [Inject] private readonly CandleSequenceController _candleSequenceController;
        [Inject] private readonly CoreMainPanelProvider _mainPanelProvider;
        [Inject] private readonly CoreEventBus _coreEventBus;
        private PriceLineProvider _priceLineProvider;
        private Vector3 _priceLinePosition;

        public void Initialize()
        {
            _priceLineProvider = _mainPanelProvider.EnterPriceLineProvider;
            _priceLineProvider.SetActive(false);

            _coreEventBus.OnPositionOpened += ShowPriceLine;
            _coreEventBus.OnPositionClosed += HidePriceLine;
            _coreEventBus.OnCameraMoved += UpdatePriceLineByCameraMoved;
        }

        public void Dispose()
        {
            _coreEventBus.OnPositionOpened -= ShowPriceLine;
            _coreEventBus.OnPositionClosed -= HidePriceLine;
            _coreEventBus.OnCameraMoved -= UpdatePriceLineByCameraMoved;
        }

        private void ShowPriceLine()
        {
            _priceLineProvider.SetActive(true);
            _priceLinePosition = _candleSequenceController.CurrentCandlePresenter.CurrentPricePosition;
            UpdateLinePosition();
        }
        
        private void UpdateLinePosition()
        {
            _priceLineProvider.UpdateLinePosition(_priceLinePosition.y);
        }

        private void HidePriceLine(PositionCloseType _)
        {
            _priceLineProvider.SetActive(false);
        }
        
        private void UpdatePriceLineByCameraMoved()
        {
            UpdateLinePosition();
        }
    }
}
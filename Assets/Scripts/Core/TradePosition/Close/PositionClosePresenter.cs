using System;
using Zenject;

namespace Core.TradePosition.Close
{
    /// <summary>
    /// Презентер для окна закрытия позиции
    /// </summary>
    public class PositionClosePresenter : IInitializable, IDisposable
    {
        [Inject] private readonly PositionCloseProvider _positionCloseProvider;
        [Inject] private readonly CoreEventBus _coreEventBus;
        
        public void Initialize()
        {
            _positionCloseProvider.OnCloseClicked += OnPositionClosePanelClosed;
        }

        public void Dispose()
        {
            _positionCloseProvider.OnCloseClicked -= OnPositionClosePanelClosed;
        }
        
        public void ShowPositionClose(PositionCloseType closeType, int pnl)
        {
            _positionCloseProvider.ShowPositionClose(closeType, pnl);
        }
        
        private void OnPositionClosePanelClosed()
        {
            _positionCloseProvider.HidePositionClosePanel();
            _coreEventBus.FireClosePositionPanelShown();
        }
    }
} 
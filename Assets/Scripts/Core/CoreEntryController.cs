using System;
using Core.Candles;
using Core.UI.Presenters;
using Core.UI.Providers;
using Zenject;

namespace Core
{
    public class CoreEntryController : IInitializable, IDisposable
    {
        [Inject] private readonly PositionSizeSelectPresenter _positionSizeSelectPresenter;
        [Inject] private readonly CandleSequenceController _candleSequenceController;
        [Inject] private readonly CurrentPositionPresenter _currentPositionPresenter;
        [Inject] private readonly CoreMainPanelProvider _mainPanelProvider;
        [Inject] private readonly BalancePresenter _balancePresenter;
        
        public void Initialize()
        {
            _mainPanelProvider.LongButton.OnButtonClicked += StartLong;
            _mainPanelProvider.ShortButton.OnButtonClicked += StartShort;
            _mainPanelProvider.ClosePositionButton.OnButtonClicked += ClosePosition;
        }
        
        public void Dispose()
        {
            _mainPanelProvider.LongButton.OnButtonClicked -= StartLong;
            _mainPanelProvider.ShortButton.OnButtonClicked -= StartShort;
            _mainPanelProvider.ClosePositionButton.OnButtonClicked -= ClosePosition;
        }
        
        private void StartLong()
        {
            StartTrade(true);
        }
        
        private void StartShort()
        {
            StartTrade(false);
        }
        
        private void StartTrade(bool isLong)
        {
            int margin = _positionSizeSelectPresenter.PositionSize;
            
            if (margin <= 0)
                return;
            
            _balancePresenter.DecreaseBalance(margin);
            
            float currentPrice = _candleSequenceController.CurrentCandlePresenter.CurrentPrice;
            _currentPositionPresenter.InitPosition(margin, currentPrice, isLong);
            _candleSequenceController.StartSpawnCandles();
        }
        
        private void ClosePosition()
        {
            int margin = _currentPositionPresenter.InitialMargin;
            int pnl = _currentPositionPresenter.Pnl;
            int result = margin + pnl;
            
            _balancePresenter.IncreaseBalance(result);
            _currentPositionPresenter.ClosePosition();
            _candleSequenceController.StopSpawn();
        }
    }
}
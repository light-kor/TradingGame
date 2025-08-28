using System;
using Common.Player;
using Core.GameOver;
using Core.TradePosition.Close;
using Zenject;

namespace Core
{
    public class ClosePositionSequenceController : IInitializable, IDisposable
    {
        [Inject] private readonly CoinInitializeFacade _coinInitializeFacade;
        [Inject] private readonly GameOverPresenter _gameOverPresenter;
        [Inject] private readonly CoreEventBus _coreEventBus;
        [Inject] private readonly MoneyFacade _moneyFacade;
        
        private PositionCloseType _currentCloseType;
        
        public void Initialize()
        {
            _coreEventBus.OnPositionClosed += HandlePositionClosed;
            _coreEventBus.OnClosePositionPanelShown += HandleClosePositionPanelShown;
            _coreEventBus.OnGameOverPanelShown += HandleGameOverPanelShown;
        }

        public void Dispose()
        {
            _coreEventBus.OnPositionClosed -= HandlePositionClosed;
            _coreEventBus.OnClosePositionPanelShown -= HandleClosePositionPanelShown;
            _coreEventBus.OnGameOverPanelShown -= HandleGameOverPanelShown;
        }

        private void HandlePositionClosed(PositionCloseType closeType)
        {
            _currentCloseType = closeType;
        }

        private void HandleClosePositionPanelShown()
        {
            if (TryGameOver())
                return;
            
            if (_currentCloseType == PositionCloseType.CoinBankrupt)
                _coinInitializeFacade.InitializeRandomCoin();
        }
        
        private bool TryGameOver()
        {
            if (IsPlayerHasMoney())
                return false;
                
            _gameOverPresenter.ShowGameOver();
            return true;
        }
        
        private void HandleGameOverPanelShown()
        {
            _moneyFacade.CreateNewMoneyFund();
            _coinInitializeFacade.InitializeRandomCoin();
        }
        
        private bool IsPlayerHasMoney()
        {
            return _moneyFacade.MoneyValue > 0;
        }
    }
}
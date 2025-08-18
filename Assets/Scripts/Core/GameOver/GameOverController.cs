using System;
using Common.Player;
using Zenject;

namespace Core.GameOver
{
    /// <summary>
    /// Контроллер для управления окончанием игры
    /// </summary>
    public class GameOverController : IInitializable, IDisposable
    {
        [Inject] private readonly CoinInitializeFacade _coinInitializeFacade;
        [Inject] private readonly GameOverPresenter _gameOverPresenter;
        [Inject] private readonly CoreEventBus _coreEventBus;
        [Inject] private readonly MoneyFacade _moneyFacade;
        
        public void Initialize()
        {
            _coreEventBus.OnClosePositionPanelShown += TryGameOver;
            _coreEventBus.OnGameOverPanelShown += RestartGame;
        }

        public void Dispose()
        {
            _coreEventBus.OnClosePositionPanelShown -= TryGameOver;
            _coreEventBus.OnGameOverPanelShown -= RestartGame;
        }
        
        private void TryGameOver()
        {
            if (IsPlayerHasMoney())
                return;
                
            _gameOverPresenter.ShowGameOver();
        }

        private bool IsPlayerHasMoney()
        {
            return _moneyFacade.MoneyValue > 0;
        }
        
        private void RestartGame()
        {
            _moneyFacade.CreateNewMoneyFund();
            _coinInitializeFacade.InitializeRandomCoin();
        }
    }
} 
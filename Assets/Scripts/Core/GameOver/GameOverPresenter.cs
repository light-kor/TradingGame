using System;
using Core.UI.Providers;
using Zenject;

namespace Core.GameOver
{
    /// <summary>
    /// Презентер для окна окончания игры
    /// </summary>
    public class GameOverPresenter : IInitializable, IDisposable
    {
        [Inject] private readonly GameOverProvider _gameOverProvider;
        [Inject] private readonly CoreEventBus _coreEventBus;
        
        public void Initialize()
        {
            _gameOverProvider.OnRestartClicked += HandleRestartClicked;
        }

        public void Dispose()
        {
            _gameOverProvider.OnRestartClicked -= HandleRestartClicked;
        }
        
        public void ShowGameOver()
        {
            _gameOverProvider.ShowGameOver();
        }

        private void HandleRestartClicked()
        {
            _gameOverProvider.HideGameOver();
            _coreEventBus.FireOnGameOverPanelShown();
        }
    }
} 
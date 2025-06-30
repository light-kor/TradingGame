using System;
using Core.UI.Providers;
using Zenject;

namespace Core
{
    public class ChangeCoinHandler : IInitializable, IDisposable
    {
        [Inject] private readonly CoinInitializeFacade _coinInitializeFacade;
        [Inject] private readonly CoreMainPanelProvider _mainPanelProvider;
        
        public void Initialize()
        {
            _mainPanelProvider.ChangeCoinButton.OnButtonClicked += ChangeCoin;
        }

        public void Dispose()
        {
            _mainPanelProvider.ChangeCoinButton.OnButtonClicked -= ChangeCoin;
        }
        
        private void ChangeCoin()
        {
            _coinInitializeFacade.InitializeRandomCoin();
        }
    }
}
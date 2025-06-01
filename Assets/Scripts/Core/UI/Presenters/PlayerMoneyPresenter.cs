using System;
using Common.Player;
using Core.UI.Providers;
using Zenject;

namespace Core.UI.Presenters
{
    public class PlayerMoneyPresenter : IInitializable, IDisposable
    {
        [Inject] private readonly CoreMainPanelProvider _mainPanelProvider;
        [Inject] private readonly MoneyFacade _moneyFacade;
        
        private PlayerMoneyProvider _moneyProvider;
        
        public void Initialize()
        {
            _moneyProvider = _mainPanelProvider.PlayerMoneyProvider;
            
            _moneyFacade.OnMoneyChanged += UpdatePlayerMoneyText;
        }

        public void Dispose()
        {
            _moneyFacade.OnMoneyChanged -= UpdatePlayerMoneyText;
        }

        private void UpdatePlayerMoneyText()
        {
            _moneyProvider.UpdateValueText(_moneyFacade.MoneyValue);
        }
    }
}
using System;
using Common.Player;
using Core.UI.Providers;
using Settings;
using Zenject;

namespace Core.UI.Presenters
{
    public class BalancePresenter : IInitializable, IDisposable
    {
        [Inject] private readonly CoreMainPanelProvider _mainPanelProvider;
        [Inject] private readonly MoneyFacade _moneyFacade;
        [Inject] private readonly GameSettings _settings;
        
        private BalanceProvider _balanceProvider;
        
        public void Initialize()
        {
            _balanceProvider = _mainPanelProvider.BalanceProvider;
            
            _moneyFacade.OnMoneyChanged += UpdatePlayerMoneyText;
            UpdatePlayerMoneyText();
        }

        public void Dispose()
        {
            _moneyFacade.OnMoneyChanged -= UpdatePlayerMoneyText;
        }

        private void UpdatePlayerMoneyText()
        {
            _balanceProvider.SetBalanceValue(_moneyFacade.MoneyValue);
        }
        
        public void IncreaseBalance(int value)
        {
            _moneyFacade.IncreaseMoney(value);
            
            string text = $"+ {value}";
            _balanceProvider.ShowFloatingText(text, _settings.LongColor);
        }
        
        public void DecreaseBalance(int value)
        {
            _moneyFacade.DecreaseMoney(value);
            
            string text = $"- {value}";
            _balanceProvider.ShowFloatingText(text, _settings.ShortColor);
        }
    }
}
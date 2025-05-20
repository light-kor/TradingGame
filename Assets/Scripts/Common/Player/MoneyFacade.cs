using System;
using UnityEngine;
using Zenject;

namespace Common.Player
{
    public class MoneyFacade : IInitializable
    {
        private const string MONEY = "Money";
        
        [Inject] private readonly FundSourceRepository _fundSourceRepository;
        
        public int MoneyValue { get; private set; }
        
        public event Action OnMoneyChanged = delegate { };
        
        public void Initialize()
        {
            if (PlayerPrefsUtils.HasKey(MONEY))
            {
                int savedMoney = PlayerPrefsUtils.LoadInt(MONEY);
                UpdateMoneyValue(savedMoney);
                Debug.LogError($"HasSavedMoney {MoneyValue}");
                return;
            }

            var source = _fundSourceRepository.GetRandomFundSource();
            UpdateMoneyValue(source.Value);
            Debug.LogError($"GetRandomFundSource {source.Header} - {source.Value}");
        }
        
        public void ChangeMoneyValue(int change)
        {
            UpdateMoneyValue(MoneyValue + change);
        }

        private void UpdateMoneyValue(int value)
        {
            MoneyValue = value;
            PlayerPrefsUtils.SaveInt(MONEY, value);
            OnMoneyChanged.Invoke();
        }
    }
}
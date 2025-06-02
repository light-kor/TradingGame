using System;
using Settings;
using UnityEngine;
using Zenject;

namespace Common.Player
{
    public class MoneyFacade : IInitializable
    {
        [Inject] private readonly FundSourceRepository _fundSourceRepository;
        
        public int MoneyValue { get; private set; }
        
        public event Action OnMoneyChanged = delegate { };
        
        public void Initialize()
        {
            if (PlayerPrefsUtils.HasKey(Const.MONEY))
            {
                int savedMoney = PlayerPrefsUtils.LoadInt(Const.MONEY);
                UpdateMoneyValue(savedMoney);
                Debug.LogError($"HasSavedMoney {MoneyValue}");
                return;
            }

            var source = _fundSourceRepository.GetRandomFundSource();
            UpdateMoneyValue(source.Value);
            Debug.LogError($"GetRandomFundSource {source.Header} - {source.Value}");
        }
        
        public void IncreaseMoney(int change)
        {
            UpdateMoneyValue(MoneyValue + change);
        }
        
        public void DecreaseMoney(int change)
        {
            UpdateMoneyValue(MoneyValue - change);
        }

        private void UpdateMoneyValue(int value)
        {
            MoneyValue = value;
            PlayerPrefsUtils.SaveInt(Const.MONEY, value);
            OnMoneyChanged.Invoke();
        }
    }
}
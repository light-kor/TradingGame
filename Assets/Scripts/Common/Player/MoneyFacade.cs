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
            if (TryLoadSavedMoney(out var savedMoney))
            {
                UpdateMoneyValue(savedMoney);
                return;
            }

            CreateNewMoneyFund();
        }

        private bool TryLoadSavedMoney(out int savedMoney)
        {
            if (PlayerPrefsUtils.HasKey(Const.MONEY))
            {
                savedMoney = PlayerPrefsUtils.LoadInt(Const.MONEY);
                Debug.LogError($"HasSavedMoney {MoneyValue}");
                return true;
            }

            savedMoney = 0;
            return false;
        }

        public void CreateNewMoneyFund()
        {
            var source = _fundSourceRepository.GetRandomFundSource();
            Debug.LogError($"GetRandomFundSource {source.Header} - {source.Value}");

            UpdateMoneyValue(source.Value);
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
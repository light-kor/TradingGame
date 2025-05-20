using TMPro;
using UnityEngine;
using Zenject;

namespace Common.Player
{
    public class MoneyProvider : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI valueText;

        [Inject] private readonly MoneyFacade _moneyFacade;
        
        private void Awake()
        {
            _moneyFacade.OnMoneyChanged += UpdatePriceText;
            UpdatePriceText();
        }

        private void OnDestroy()
        {
            _moneyFacade.OnMoneyChanged -= UpdatePriceText;
        }

        private void UpdatePriceText()
        {
            float price = _moneyFacade.MoneyValue;
            string newText = $"{price:F2} $";
            valueText.SetText(newText);
        }
    }
}
using Core.Candles;
using UnityEngine;
using Zenject;
using TMPro;

namespace Core.UI
{
    public class CoinPriceProvider : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI priceText;

        [Inject] private readonly CoreEventBus _coreEventBus;
        
        private void Awake()
        {
            _coreEventBus.OnCurrentPriceUpdated += UpdatePriceText;
        }

        private void OnDestroy()
        {
            _coreEventBus.OnCurrentPriceUpdated -= UpdatePriceText;
        }

        private void UpdatePriceText(CandlePresenter currentCandle)
        {
            float price = currentCandle.CurrentPrice;
            string newText = $"{price:F2} $";
            priceText.SetText(newText);
        }
    }
}
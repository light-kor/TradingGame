using Core.Candles;
using TMPro;
using UnityEngine;
using Zenject;

namespace Core.UI.Providers
{
    //TODO: Нужен презентер
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
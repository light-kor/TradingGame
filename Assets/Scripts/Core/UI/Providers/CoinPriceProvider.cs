using TMPro;
using TriInspector;
using UnityEngine;

namespace Core.UI.Providers
{
    public class CoinPriceProvider : MonoBehaviour
    {
        [Required]
        [SerializeField] 
        private TextMeshProUGUI priceText;
        
        public void UpdatePriceText(float currentPrice)
        {
            string newText = $"{currentPrice:F2} $";
            priceText.SetText(newText);
        }
    }
}
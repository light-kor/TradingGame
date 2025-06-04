using Core.UI.Common;
using TriInspector;
using UnityEngine;

namespace Core.UI.Providers
{
    public class BalanceProvider : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private CurrencyProvider balanceValueProvider;
        
        [Required]
        [SerializeField]
        private FloatingTextProvider floatingTextProvider;

        public void SetBalanceValue(int value)
        {
            balanceValueProvider.SetIntValue(value);
        }
        
        public void ShowFloatingText(string text, Color color)
        {
            floatingTextProvider.ShowText(text, color);
        }
    }
}
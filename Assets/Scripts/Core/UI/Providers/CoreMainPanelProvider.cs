using Common.Player;
using Core.UI.Common;
using TriInspector;
using UnityEngine;

namespace Core.UI.Providers
{
    public class CoreMainPanelProvider : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private ButtonProvider continueButton;
        
        [Required]
        [SerializeField]
        private MoneyProvider playerMoneyProvider;
        
        [Required]
        [SerializeField]
        private PriceLineProvider priceLineProvider;
        
        public ButtonProvider ContinueButton => continueButton;
        public MoneyProvider PlayerMoneyProvider => playerMoneyProvider;
        public PriceLineProvider PriceLineProvider => priceLineProvider;
    }
}

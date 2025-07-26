using Core.UI.Common;
using TriInspector;
using UnityEngine;

namespace Core.UI.Providers
{
    public class CoreMainPanelProvider : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private BalanceProvider balanceProvider;
        
        [Required]
        [SerializeField]
        private CurrencyProvider coinPriceProvider;
        
        [Required]
        [SerializeField]
        private PriceLineProvider currentPriceLineProvider;
        
        [Required]
        [SerializeField]
        private PriceLineProvider enterPriceLineProvider;
        
        [Required]
        [SerializeField]
        private PositionSizeSelectProvider positionSizeSelectProvider;
        
        [Required]
        [SerializeField]
        private ButtonProvider longButton;
        
        [Required]
        [SerializeField]
        private ButtonProvider shortButton;
        
        [Required]
        [SerializeField]
        private ButtonProvider closePositionButton;
        
        [Required]
        [SerializeField]
        private CurrentPositionProvider currentPositionProvider;
        
        [Required]
        [SerializeField]
        private ButtonProvider changeCoinButton;
        
        [Required]
        [SerializeField]
        private ButtonProvider riskInfoButton;
        
        public BalanceProvider BalanceProvider => balanceProvider;
        public CurrencyProvider CoinPriceProvider => coinPriceProvider;
        public PriceLineProvider CurrentPriceLineProvider => currentPriceLineProvider;
        public PriceLineProvider EnterPriceLineProvider => enterPriceLineProvider;
        public PositionSizeSelectProvider PositionSizeSelectProvider => positionSizeSelectProvider;
        public ButtonProvider LongButton => longButton;
        public ButtonProvider ShortButton => shortButton;
        public ButtonProvider ClosePositionButton => closePositionButton;
        public CurrentPositionProvider CurrentPositionProvider => currentPositionProvider;
        public ButtonProvider ChangeCoinButton => changeCoinButton;
        public ButtonProvider RiskInfoButton => riskInfoButton;
    }
}

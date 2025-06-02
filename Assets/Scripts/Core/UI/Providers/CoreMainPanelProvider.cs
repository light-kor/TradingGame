using Core.UI.Common;
using TriInspector;
using UnityEngine;

namespace Core.UI.Providers
{
    public class CoreMainPanelProvider : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private CurrencyProvider playerMoneyProvider;
        
        [Required]
        [SerializeField]
        private CurrencyProvider coinPriceProvider;
        
        [Required]
        [SerializeField]
        private PriceLineProvider priceLineProvider;
        
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
        
        public CurrencyProvider PlayerMoneyProvider => playerMoneyProvider;
        public CurrencyProvider CoinPriceProvider => coinPriceProvider;
        public PriceLineProvider PriceLineProvider => priceLineProvider;
        public PositionSizeSelectProvider PositionSizeSelectProvider => positionSizeSelectProvider;
        public ButtonProvider LongButton => longButton;
        public ButtonProvider ShortButton => shortButton;
        public ButtonProvider ClosePositionButton => closePositionButton;
        public CurrentPositionProvider CurrentPositionProvider => currentPositionProvider;
    }
}

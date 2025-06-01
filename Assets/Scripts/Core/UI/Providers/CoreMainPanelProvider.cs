using Core.UI.Common;
using TriInspector;
using UnityEngine;

namespace Core.UI.Providers
{
    public class CoreMainPanelProvider : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private ButtonProvider startSpawnButton;
        
        [Required]
        [SerializeField]
        private ButtonProvider stopSpawnButton;
        
        [Required]
        [SerializeField]
        private PlayerMoneyProvider playerMoneyProvider;
        
        [Required]
        [SerializeField]
        private CoinPriceProvider coinPriceProvider;
        
        [Required]
        [SerializeField]
        private PriceLineProvider priceLineProvider;
        
        public ButtonProvider StartSpawnButton => startSpawnButton;
        public ButtonProvider StopSpawnButton => stopSpawnButton;
        public PlayerMoneyProvider PlayerMoneyProvider => playerMoneyProvider;
        public CoinPriceProvider CoinPriceProvider => coinPriceProvider;
        public PriceLineProvider PriceLineProvider => priceLineProvider;
    }
}

using Settings;
using UnityEngine;
using Zenject;

namespace Core
{
    public class CurrentCoinFacade
    {
        [Inject] private readonly PriceMovePatternsRepository _priceMovePatternsRepository;

        private PriceMovePatternSettings _currentPattern;
        
        public void CreateCurrentCoin(PriceMoveRiskType type =  PriceMoveRiskType.Middle)
        {
            _currentPattern = _priceMovePatternsRepository.GetPriceMovePattern(type);
            Debug.LogError($"Use pattern {_currentPattern.name}");
        }
        
        public PriceMovePatternSettings GetCurrentPattern()
        {
            return _currentPattern;
        }
    }
}
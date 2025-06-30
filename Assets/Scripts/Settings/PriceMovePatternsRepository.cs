using System;
using System.Collections.Generic;
using TriInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Settings
{
    [CreateAssetMenu(fileName = "PriceMovePatternsRepository", menuName = "Settings/PriceMovePatternsRepository")]
    public class PriceMovePatternsRepository : ScriptableObject
    {
        [field: Required]
        [field: SerializeField]
        public PriceMovePatternSettings DefaultPattern { get; private set; }
        
        [field: Space]
        [field: SerializeField]
        public List<PriceMovePatternSettings> LowRickPatterns { get; private set; }
        
        [field: Required]
        [field: SerializeField]
        public List<PriceMovePatternSettings> MidRickPatterns { get; private set; }
        
        [field: Required]
        [field: SerializeField]
        public List<PriceMovePatternSettings> HighRickPatterns { get; private set; }
        
        [field: Required]
        [field: SerializeField]
        public List<PriceMovePatternSettings> InsaneRickPatterns { get; private set; }

        public PriceMovePatternSettings GetPriceMovePattern(PriceMoveRiskType type)
        {
            var patterns = GetPatternsByRiskType(type);
            int randomIndex = Random.Range(0, patterns.Count);
            return patterns[randomIndex];
        }

        private List<PriceMovePatternSettings> GetPatternsByRiskType(PriceMoveRiskType type)
        {
            return type switch
            {
                PriceMoveRiskType.Low => LowRickPatterns,
                PriceMoveRiskType.Middle => MidRickPatterns,
                PriceMoveRiskType.High => HighRickPatterns,
                PriceMoveRiskType.Insane => InsaneRickPatterns,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}
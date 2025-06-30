using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "PriceMovePatternSettings", menuName = "Settings/PriceMovePatternSettings")]
    public class PriceMovePatternSettings : ScriptableObject
    {
        [field: SerializeField]
        public float InitialPrice { get; private set; }

        [field: SerializeField]
        public float MinBodyPercent { get; private set; }
        
        [field: SerializeField]
        public float MaxBodyPercent { get; private set; }
        
        [field: SerializeField]
        public float MaxWickPercent { get; private set; }
        
        [field: SerializeField]
        public float VisualMultiplier { get; private set; }
    }
}
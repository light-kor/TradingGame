using Core.Candles;
using TriInspector;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Settings/GameSettings")]
    public class GameSettings : ScriptableObject
    {
        [field: Title("Main settings")] 
        [field: SerializeField]
        public int CandlesPoolCount { get; private set; }
        
        [field: SerializeField]
        public float CandleSpawnOffset { get; private set; }
        
        [field: Title("Prefabs")] 
        [field: Required]
        [field: SerializeField]
        public CandleProvider CandlePrefab { get; private set; }
        
        [field: SerializeField]
        public Color LongColor { get; private set; }

        [field: SerializeField]
        public Color ShortColor { get; private set; }
        
        [field: Title("Default candle width")] 
        [field: SerializeField]
        public float BodyWidth { get; private set; }
        
        [field: SerializeField]
        public float WickWidth { get; private set; }
        
        [field: Title("Candle price settings")] 
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
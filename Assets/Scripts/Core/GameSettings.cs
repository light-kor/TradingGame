using Core.Candles;
using DG.Tweening;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Core/GameSettings")]
    public class GameSettings : ScriptableObject
    {
        [field: Header("Main settings")] [field: SerializeField]
        public int CandlesPoolCount { get; private set; }
        
        [field: SerializeField]
        public int CandlesSpawnCount { get; private set; }
        
        [field: Header("Prefabs")] 
        [field: SerializeField]
        public CandleProvider CandlePrefab { get; private set; }

        [field: SerializeField]
        public Color LongColor { get; private set; }

        [field: SerializeField]
        public Color ShortColor { get; private set; }
        
        [field: Header("Candle size")] 
        [field: SerializeField]
        public float BodyWidth { get; private set; }
        
        [field: SerializeField]
        public float WickWidth { get; private set; }
        
        [field: Header("Animation")] 
        [field: SerializeField]
        public float AnimationDuration { get; private set; }
        
        [field: SerializeField]
        public float CandleSpawnOffset { get; private set; }
        
        [field: SerializeField]
        public Ease AnimationEase { get; private set; }
        
        [field: Header("Candle size")] 
        [field: SerializeField]
        public float MinBodySize { get; private set; }

        [field: SerializeField]
        public float MaxBodySize { get; private set; }
        
        [field: SerializeField]
        public float MaxWickSize { get; private set; }
    }
}
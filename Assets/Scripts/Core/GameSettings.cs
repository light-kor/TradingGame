using Core.Candles;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Core/GameSettings")]
    public class GameSettings : ScriptableObject
    {
        [SerializeField] 
        public CandleProvider CandlePrefab;

        [SerializeField] 
        public Color LongColor;

        [SerializeField] 
        public Color ShortColor;

        [SerializeField] 
        public float AnimationDuration;
        
        [SerializeField] 
        public float XSpawnOffset;
        
        [SerializeField] 
        [Header("Candle size")] 
        public float BodyXSize;
        
        [SerializeField]
        public float WickXSize;
        
        [SerializeField] 
        [Header("Price random")] 
        public float MinBodyValue;

        [SerializeField]
        public float MaxBodyValue;
        
        [SerializeField]
        public float MaxWickValue;
    }
}
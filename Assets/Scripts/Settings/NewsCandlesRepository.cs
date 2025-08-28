using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Settings
{
    [CreateAssetMenu(fileName = "NewsCandlesRepository", menuName = "Settings/NewsCandlesRepository")]
    public class NewsCandlesRepository : ScriptableObject
    {
        [field: SerializeField]
        [field: Range(0f, 1f)]
        public float NewsCandleChance { get; private set; }
        
        [field: SerializeField]
        public List<NewsCandleInfo> PositiveNewsCandles { get; private set; }
        
        [field: SerializeField]
        public List<NewsCandleInfo> NegativeNewsCandles { get; private set; }
        
        public NewsCandleInfo GetRandomNewsCandle(bool isLong)
        {
            var candles = isLong ? PositiveNewsCandles : NegativeNewsCandles;
            
            if (candles.Count == 0)
            {
                Debug.LogError($"Нет новостных свечей для типа: {(isLong ? "Positive" : "Negative")}");
                return null;
            }
            
            int randomIndex = Random.Range(0, candles.Count);
            return candles[randomIndex];
        }
    }
} 
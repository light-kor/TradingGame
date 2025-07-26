using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "NewsCandle", menuName = "Settings/NewsCandle")]
    public class NewsCandleInfo : ScriptableObject
    {
        [field: SerializeField]
        public string Title { get; private set; }
        
        [field: SerializeField]
        [field: TextArea(3, 5)]
        public string Description { get; private set; }
        
        [field: SerializeField]
        public bool IsLong { get; private set; }
        
        [field: SerializeField]
        [field: Range(1f, 20f)]
        public float VolatilityMultiplier { get; private set; }
    }
} 
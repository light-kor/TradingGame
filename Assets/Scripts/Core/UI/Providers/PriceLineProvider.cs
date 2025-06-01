using Core.Candles;
using TriInspector;
using UnityEngine;

namespace Core.UI.Providers
{
    public class PriceLineProvider : MonoBehaviour
    {
        [Required]
        [SerializeField] 
        public Transform lineTransform;
        
        public void UpdateLinePosition(CandlePresenter currentCandle)
        {
            var currentPrice = currentCandle.CurrentPrice;
            
            var linePosition = lineTransform.position;
            linePosition.y = currentPrice;
            lineTransform.position = linePosition;
        }
    }
}
using Core.Candles;
using UnityEngine;

namespace Core.UI.Providers
{
    public class PriceLineProvider : MonoBehaviour
    {
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
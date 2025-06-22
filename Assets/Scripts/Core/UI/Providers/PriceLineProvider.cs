using TriInspector;
using UnityEngine;

namespace Core.UI.Providers
{
    public class PriceLineProvider : MonoBehaviour
    {
        [Required]
        [SerializeField] 
        private Transform lineTransform;

        public void SetActive(bool state)
        {
            gameObject.SetActive(state);
        }
        
        public void UpdateLinePosition(float priceYPosition)
        {
            var linePosition = lineTransform.position;
            linePosition.y = priceYPosition;
            lineTransform.position = linePosition;
        }
    }
}
using UnityEngine;

namespace Core.Candles
{
    public class CandleProvider : MonoBehaviour
    {
        [SerializeField] 
        private SpriteRenderer body;

        [SerializeField] 
        private SpriteRenderer wick;
        
        public Transform BodyTransform => body.transform;
        public Transform WickTransform => wick.transform;

        public void SetColor(Color color)
        {
            body.color = color;
            wick.color = color;
        }

        public void ResetCandleSize(GameSettings settings)
        {
            body.transform.localScale = new Vector3(settings.BodyWidth, 0f, 1f);
            wick.transform.localScale = new Vector3(settings.WickWidth, 0f, 1f);
        }

        public void SetPosition(Vector3 pos)
        {
            transform.localPosition = pos;
        }
    }
}
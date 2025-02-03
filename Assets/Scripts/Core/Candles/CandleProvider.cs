using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace Core.Candles
{
    public class CandleProvider : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer body;

        [SerializeField] public SpriteRenderer wick;

        private CandlePriceSettings _priceSettings;
        private GameSettings _settings;
        private float _savedWickLocalY;

        public void InitCandle(CandlePriceSettings priceSettings, GameSettings settings)
        {
            _priceSettings = priceSettings;
            _settings = settings;
            SetZeroYScale();
        }

        private void SetColor(bool isLong)
        {
            Color candleColor = isLong ? _settings.LongColor : _settings.ShortColor;
            body.color = candleColor;
            wick.color = candleColor;
        }

        private void SetZeroYScale()
        {
            body.transform.localScale = new Vector3(_settings.BodyXSize, 0f, 1f);
            wick.transform.localScale = new Vector3(_settings.WickXSize, 0f, 1f);
        }

        public void SetPosition(int xPos, float lastCurrentClosePrice)
        {
            transform.position = new Vector3(xPos * _settings.XSpawnOffset, lastCurrentClosePrice, 0);
        }

        public float GetClosePosition()
        {
            return _priceSettings.ClosePrice;
        }

        private TweenerCore<Vector3, Vector3, VectorOptions> ScaleCandle(float endValue, bool isLong, float animTime)
        {
            return body.transform.DOScaleY(endValue, animTime).SetEase(Ease.Linear)
                .OnUpdate(() => SyncBodyPositionWithScale(isLong));
        }

        private void SyncBodyPositionWithScale(bool isLong)
        {
            var bodyTransform = body.transform;
            var currentYPosition = bodyTransform.localScale.y / 2f;
            var newPosition = bodyTransform.localPosition;
            newPosition.y = isLong ? currentYPosition : -currentYPosition;
            bodyTransform.localPosition = newPosition;
        }
        
        private void SyncWickWithScaleFirst(bool isLong)
        {
            var wickTransform = wick.transform;
            var currentYPosition = wickTransform.localScale.y / 2f;
            var newPosition = wickTransform.localPosition;
            newPosition.y = isLong ? currentYPosition : -currentYPosition;
            wickTransform.localPosition = newPosition;
        }
        
        private void SyncWickWithScaleSecond(bool isLong)
        {
            int sign = isLong ? 1 : -1;
            var wickTransform = wick.transform;
            var currentYPosition = _savedWickLocalY + (wickTransform.localScale.y * sign);
            var newPosition = wickTransform.localPosition;
            newPosition.y = currentYPosition;
            wickTransform.localPosition = newPosition;
        }

        public void AnimateCandle()
        {
            float lowScale = Mathf.Abs(_priceSettings.LowPrice);
            float highScale = Mathf.Abs(_priceSettings.HighPrice);
            float closeScale = Mathf.Abs(_priceSettings.ClosePrice);
            
            if (_priceSettings.IsLong)
            {
                float scaleMoveValue = lowScale + lowScale + highScale + (highScale - closeScale);
                float lowScaleTime = (lowScale / scaleMoveValue) * _settings.AnimationDuration;
                float highScaleTime = (highScale / scaleMoveValue) * _settings.AnimationDuration;
                float closeScaleTime = ((highScale - closeScale) / scaleMoveValue) * _settings.AnimationDuration;
                    
                Sequence animSequence = DOTween.Sequence();
                animSequence.AppendCallback(() => SetColor(false));
                
                animSequence.Append(ScaleCandle(lowScale, false, lowScaleTime));//.OnUpdate(() => SyncWickWithScaleFirst(false));
                animSequence.Append(ScaleCandle(0, false, lowScaleTime));
                
                animSequence.AppendCallback(() => _savedWickLocalY = wick.transform.localPosition.y);
                animSequence.AppendCallback(() => SetColor(true));
                
                animSequence.Append(ScaleCandle(highScale, true, highScaleTime));//.OnUpdate(SyncWickWithScaleSecond);
                animSequence.Append(ScaleCandle(closeScale, true, closeScaleTime));
                animSequence.Play();
                
            }
            else
            {
                float scaleMoveValue = highScale + highScale + lowScale + (lowScale - closeScale);
                float highScaleTime = (highScale / scaleMoveValue) * _settings.AnimationDuration;
                float lowScaleTime = (lowScale / scaleMoveValue) * _settings.AnimationDuration;
                float closeScaleTime = ((lowScale - closeScale) / scaleMoveValue) * _settings.AnimationDuration;
                    
                // Шортовая свеча: сначала до HighPrice, потом до LowPrice, затем до ClosePrice
                Sequence animSequence = DOTween.Sequence();
                animSequence.AppendCallback(() => SetColor(true));
                
                animSequence.Append(ScaleCandle(highScale, true, highScaleTime));//.OnUpdate(() => SyncWickWithScaleFirst(false));
                animSequence.Append(ScaleCandle(0, true, highScaleTime));
                
                animSequence.AppendCallback(() => _savedWickLocalY = wick.transform.localPosition.y);
                animSequence.AppendCallback(() => SetColor(false));
                
                animSequence.Append(ScaleCandle(lowScale, false, lowScaleTime));//.OnUpdate(SyncWickWithScaleSecond);
                animSequence.Append(ScaleCandle(closeScale, false, closeScaleTime));
                animSequence.Play();
            }
        }
    }
}
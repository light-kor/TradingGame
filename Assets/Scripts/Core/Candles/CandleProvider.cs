using System.Collections;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace Core.Candles
{
    public class CandleProvider : MonoBehaviour
    {
        [SerializeField] 
        private SpriteRenderer body;

        [SerializeField] 
        private SpriteRenderer wick;
        
        [field: SerializeField]
        public SpriteRenderer WickWidth { get; private set; }

        private CandlePriceSettings _priceSettings;
        private GameSettings _settings;
        private float _savedWickLocalY;
        private bool _isWickSaved;

        public float ClosePrice => _priceSettings.ClosePrice;

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
            body.transform.localScale = new Vector3(_settings.BodyWidth, 0f, 1f);
            wick.transform.localScale = new Vector3(_settings.WickWidth, 0f, 1f);
        }

        public void SetPosition(int xPos, float lastCurrentClosePrice)
        {
            transform.localPosition = new Vector3(xPos * _settings.CandleSpawnOffset, lastCurrentClosePrice, 0);
        }

        private TweenerCore<Vector3, Vector3, VectorOptions> ScaleCandle(float targetScale, bool isAboveZero,
            float duration)
        {
            return body.transform.DOScaleY(targetScale, duration)
                .SetEase(Ease.Linear) //TODO: Выбрать лучший вариант
                .OnUpdate(() => UpdateBodyPosition(isAboveZero));
        }

        private void UpdateBodyPosition(bool isAboveZero)
        {
            float currentScale = Mathf.Abs(body.transform.localScale.y);
            float halfHeight = currentScale / 2f;
            Vector3 newLocalPos = body.transform.localPosition;
            
            // Если анимация "растёт вверх" – нижняя сторона остаётся на уровне 0, значит центр = halfHeight.
            // Если вниз – верхняя сторона зафиксирована, значит центр = -halfHeight.
            newLocalPos.y = isAboveZero ? halfHeight : -halfHeight;
            body.transform.localPosition = newLocalPos;
        }
        
        private void SetWickScaleSizeAndPosition(float targetScale, bool isAboveZero)
        {
            float currentScale = targetScale;

            if (_isWickSaved)
                currentScale += _savedWickLocalY;
            else
            {
                _savedWickLocalY = targetScale;
                _isWickSaved = true;
            }
            
            var wickTransform = wick.transform;
            Vector3 newLocalPos = wickTransform.localPosition;

            if (!_isWickSaved)
            {
                float halfHeight = currentScale / 2f;
                newLocalPos.y = isAboveZero ? halfHeight : -halfHeight;
            }
            else
            {
                float halfHeight = targetScale / 2f;
                newLocalPos.y += isAboveZero ? halfHeight : -halfHeight;
            }
            
            wickTransform.localPosition = newLocalPos;
                
            Vector3 localScale = wickTransform.localScale;
            localScale.y = currentScale;
            wickTransform.localScale = localScale;
        }

        public IEnumerator AnimateCandle()
        {
            float firstTargetScale;
            float secondTargetScale = 0f;
            float thirdTargetScale;
            float fourthTargetScale = Mathf.Abs(_priceSettings.ClosePrice - _priceSettings.OpenPrice);

            bool isLong = _priceSettings.IsLong;
            
            if (isLong)
            {
                firstTargetScale = Mathf.Abs(_priceSettings.LowPrice - _priceSettings.OpenPrice);
                thirdTargetScale = Mathf.Abs(_priceSettings.HighPrice - _priceSettings.OpenPrice);

            }
            else
            {
                firstTargetScale = Mathf.Abs(_priceSettings.HighPrice - _priceSettings.OpenPrice);
                thirdTargetScale = Mathf.Abs(_priceSettings.LowPrice - _priceSettings.OpenPrice);
            }

            Sequence animSequence = DOTween.Sequence();

            float scaleMoveValue = firstTargetScale + firstTargetScale + thirdTargetScale + (thirdTargetScale - fourthTargetScale);
            float firstScaleTime = (firstTargetScale / scaleMoveValue) * _settings.AnimationDuration;
            float secondScaleTime = firstScaleTime;
            float thirdScaleTime = (thirdTargetScale / scaleMoveValue) * _settings.AnimationDuration;
            float fourthScaleTime = ((thirdTargetScale - fourthTargetScale) / scaleMoveValue) * _settings.AnimationDuration;

            animSequence.AppendCallback(() => SetColor(!isLong));

            animSequence.Append(ScaleCandle(firstTargetScale, !isLong, firstScaleTime)); 
            animSequence.AppendCallback(() => SetWickScaleSizeAndPosition(firstTargetScale, !isLong));
            animSequence.Append(ScaleCandle(secondTargetScale, !isLong, secondScaleTime));

            animSequence.AppendCallback(() => SetColor(isLong));

            animSequence.Append(ScaleCandle(thirdTargetScale, isLong, thirdScaleTime));
            animSequence.AppendCallback(() => SetWickScaleSizeAndPosition(thirdTargetScale, isLong));
            animSequence.Append(ScaleCandle(fourthTargetScale, isLong, fourthScaleTime));
            animSequence.Play();

            yield return animSequence.WaitForCompletion();
        }
    }
}
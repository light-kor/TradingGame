using Settings;
using UnityEngine;
using Zenject;

namespace Core.Candles.SpawnFacade
{
    public abstract class CandleSpawnFacadeBase
    {
        [Inject] private readonly GameSettings _settings;

        protected Color GetColorByDirection(bool isLong)
        {
            return isLong ? _settings.LongColor : _settings.ShortColor;
        }
        
        protected virtual void UpdateCurrentPrice(CandlePresenter presenter, bool isAboveZero)
        {
            float currentScaleY = presenter.Provider.BodyTransform.localScale.y;
            float currentScaleYWithSign = isAboveZero ? currentScaleY : -currentScaleY;
            
            presenter.UpdateCurrentPrice(currentScaleYWithSign);
        }

        protected void UpdateBodyPosition(CandleProvider provider, bool isAboveZero)
        {
            float currentScale = Mathf.Abs(provider.BodyTransform.localScale.y);
            float halfHeight = currentScale / 2f;
            Vector3 newLocalPos = provider.BodyTransform.localPosition;

            // Если анимация растёт вверх – центр устанавливается на halfHeight, иначе – -halfHeight.
            newLocalPos.y = isAboveZero ? halfHeight : -halfHeight;
            provider.BodyTransform.localPosition = newLocalPos;
        }

        protected void SetWickScaleSizeAndPosition(CandleProvider provider, float targetScale,
            bool isAboveZero, float additionalScale = 0f)
        {
            float currentScale = targetScale + additionalScale;

            Transform wickTransform = provider.WickTransform;
            SetWickLocalPosition(isAboveZero, wickTransform, targetScale, additionalScale);
            SetWickScale(wickTransform, currentScale);
        }

        private void SetWickLocalPosition(bool isAboveZero, Transform wickTransform, float targetScale, float additionalScale)
        {
            Vector3 newLocalPos = wickTransform.localPosition;

            // Для тени в другую сторону логика чуть сложнее, вот и разделение
            if (Mathf.Approximately(additionalScale, 0f))
            {
                float halfHeight = (targetScale + additionalScale) / 2f;
                newLocalPos.y = isAboveZero ? halfHeight : -halfHeight;
            }
            else
            {
                float halfHeight = targetScale / 2f;
                newLocalPos.y += isAboveZero ? halfHeight : -halfHeight;
            }

            wickTransform.localPosition = newLocalPos;
        }

        private void SetWickScale(Transform wickTransform, float currentScale)
        {
            Vector3 localScale = wickTransform.localScale;
            localScale.y = currentScale;
            wickTransform.localScale = localScale;
        }
    }
}
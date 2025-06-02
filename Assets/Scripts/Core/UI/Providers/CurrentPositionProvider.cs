using Core.UI.Common;
using TriInspector;
using UnityEngine;

namespace Core.UI.Providers
{
    public class CurrentPositionProvider : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private CurrencyProvider initialMarginProvider;
        
        [Required]
        [SerializeField]
        private CurrencyProvider roiProvider;
        
        [Required]
        [SerializeField]
        private CurrencyProvider pnlProvider;

        public void SetActive(bool state)
        {
            gameObject.SetActive(state);
        }

        public void SetPositionMargin(int margin)
        {
            initialMarginProvider.SetIntValue(margin);
        }
        
        public void UpdateCurrentRoi(float roiValue, Color textColor)
        {
            roiProvider.SetValuePercent(roiValue);
            roiProvider.SetColor(textColor);
        }
        
        public void UpdateCurrentPnl(int pnlValue)
        {
            pnlProvider.SetIntValue(pnlValue);
        }
    }
}
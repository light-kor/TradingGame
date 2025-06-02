using Core.UI.Common;
using TriInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Core.UI.Providers
{
    public class PositionSizeSelectProvider : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private Slider slider;
        
        [Required]
        [SerializeField]
        private CurrencyProvider positionSize;
        
        public void ConfigureSlider(int moneyValue)
        {
            slider.value = 0;
            slider.minValue = 0;
            slider.maxValue = moneyValue;
            slider.wholeNumbers = true;
        }
        
        public void SubscribeSlider(UnityAction<float> action)
        {
            slider.onValueChanged.AddListener(action);
        }
        
        public void UnsubscribeSlider(UnityAction<float> action)
        {
            slider.onValueChanged.RemoveListener(action);
        }
        
        public void SetPositionSizeValue(int value)
        {
            positionSize.SetIntValue(value);
        }
    }
}
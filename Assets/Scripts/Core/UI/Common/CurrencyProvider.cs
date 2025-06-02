using Settings;
using TMPro;
using TriInspector;
using UnityEngine;

namespace Core.UI.Common
{
    public class CurrencyProvider : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private TextMeshProUGUI valueText;

        public void SetValue(float value)
        {
            string text = $"{value:F2} {Const.USDR}";
            valueText.SetText(text);
        }
        
        public void SetValuePercent(float value)
        {
            string text = $"{value:F2} %";
            valueText.SetText(text);
        }
        
        public void SetIntValue(int value)
        {
            string text = $"{value} {Const.USDR}";
            valueText.SetText(text);
        }
        
        public void SetColor(Color color)
        {
            valueText.color = color;
        }
    }
}
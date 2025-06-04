using TMPro;
using TriInspector;
using UnityEngine;

namespace Core.UI.Common
{
    public class FloatingTextProvider : MonoBehaviour
    {
        [Required] 
        [SerializeField] 
        private TextMeshProUGUI valueText;

        private void Awake()
        {
            SetActive(false);
        }

        private void SetActive(bool state)
        {
            gameObject.SetActive(state);
        }

        public void ShowText(string text, Color color = default)
        {
            SetActive(false);

            valueText.SetText(text);

            if (color != default)
                valueText.color = color;

            SetActive(true);
        }

        public void HandleAnimationEnd()
        {
            SetActive(false);
        }
    }
}
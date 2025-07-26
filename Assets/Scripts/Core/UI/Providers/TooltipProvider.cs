using TMPro;
using TriInspector;
using UnityEngine;
using Core.UI.Common;

namespace Core.UI.Providers
{
    public class TooltipProvider : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private TextMeshProUGUI text;
        
        [Required]
        [SerializeField]
        private ButtonProvider closeButton;

        private void Awake()
        {
            HideTooltip();
            closeButton.OnButtonClicked += HideTooltip;
        }

        public void ShowTooltip(string info)
        {
            text.SetText(info);
            SetActive(true);
        }
        
        private void HideTooltip()
        {
            SetActive(false);
        }
        
        private void SetActive(bool state)
        {
            gameObject.SetActive(state);
        }

        private void OnDestroy()
        {
            closeButton.OnButtonClicked -= HideTooltip;
        }
    }
}
using System;
using TriInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.Providers
{
    public class NewsPopupProvider : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private TextMeshProUGUI titleText;
        
        [Required]
        [SerializeField]
        private TextMeshProUGUI descriptionText;
        
        [Required]
        [SerializeField]
        private Image directionIndicator;
        
        [Required]
        [SerializeField]
        private RectTransform popupRectTransform;
        
        public RectTransform PopupRectTransform => popupRectTransform;

        private void Awake()
        {
            SetActive(false);
        }

        public void SetActive(bool state)
        {
            gameObject.SetActive(state);
        }

        public void SetTitle(string title)
        {
            titleText.SetText(title);
        }
        
        public void SetDescription(string description)
        {
            descriptionText.SetText(description);
        }
        
        public void SetDirectionColor(Color color)
        {
            directionIndicator.color = color;
        }
    }
} 
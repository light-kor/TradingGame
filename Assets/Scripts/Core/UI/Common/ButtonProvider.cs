using System;
using TriInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.Common
{
    public class ButtonProvider : MonoBehaviour
    {
        [Required]
        [SerializeField] 
        private Button button;

        public event Action OnButtonClicked = delegate {  }; 
        
        private void Start()
        {
            button.onClick.AddListener(HandleButtonClick);
        }
        
        private void HandleButtonClick()
        {
            OnButtonClicked.Invoke();
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(HandleButtonClick);
        }
    }
}
using System;
using Core.UI.Common;
using TMPro;
using TriInspector;
using UnityEngine;

namespace Core.TradePosition.Close
{
    /// <summary>
    /// Провайдер для UI окна закрытия позиции
    /// </summary>
    public class PositionCloseProvider : MonoBehaviour
    {
        [Header("UI Elements")]
        [Required]
        [SerializeField] 
        private GameObject positionClosePanel;
        
        [Required]
        [SerializeField] 
        private TextMeshProUGUI titleText;
        
        [Required]
        [SerializeField] 
        private TextMeshProUGUI descriptionText;
        
        [Required]
        [SerializeField] 
        private TextMeshProUGUI pnlText;
        
        [Required]
        [SerializeField] 
        private ButtonProvider closeButton;
        
        [Header("Position Close Messages")]
        [SerializeField] 
        private string manualCloseTitle = "Позиция закрыта";
        
        [SerializeField] 
        private string manualCloseDescription = "Ваша позиция была закрыта.";
        
        [SerializeField] 
        private string positionLiquidatedTitle = "Позиция ликвидирована!";
        
        [SerializeField] 
        private string positionLiquidatedDescription = "Ваша позиция была ликвидирована из-за критических убытков.";
        
        [SerializeField] 
        private string coinBankruptTitle = "Монета обанкротилась!";
        
        [SerializeField] 
        private string coinBankruptDescription = "Цена монеты упала до нуля. Все позиции закрыты.";
        
        public event Action OnCloseClicked = delegate { };
        
        private void Awake()
        {
            closeButton.OnButtonClicked += HandleCloseClicked;
        }
        
        private void OnDestroy()
        {
            closeButton.OnButtonClicked -= HandleCloseClicked;
        }
        
        private void HandleCloseClicked()
        {
            OnCloseClicked.Invoke();
        }

        public void ShowPositionClose(PositionCloseType closeType, int pnl)
        {
            SetActive(true);
            
            string pnlString = pnl >= 0 ? $"+{pnl}" : pnl.ToString();
            pnlText.SetText($"PnL: {pnlString}");
            pnlText.color = pnl >= 0 ? Color.green : Color.red;
            
            switch (closeType)
            {
                case PositionCloseType.Manual:
                    titleText.SetText(manualCloseTitle);
                    descriptionText.SetText(manualCloseDescription);
                    break;
                    
                case PositionCloseType.PositionLiquidated:
                    titleText.SetText(positionLiquidatedTitle);
                    descriptionText.SetText(positionLiquidatedDescription);
                    break;
                    
                case PositionCloseType.CoinBankrupt:
                    titleText.SetText(coinBankruptTitle);
                    descriptionText.SetText(coinBankruptDescription);
                    break;
            }
        }
        
        public void HidePositionClosePanel()
        {
            SetActive(false);
        }
        
        private void SetActive(bool state)
        {
            positionClosePanel.SetActive(state);
        }
    }
} 
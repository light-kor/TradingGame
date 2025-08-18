using System;
using Core.UI.Common;
using TMPro;
using TriInspector;
using UnityEngine;

namespace Core.GameOver
{
    /// <summary>
    /// Провайдер для UI окна окончания игры
    /// </summary>
    public class GameOverProvider : MonoBehaviour
    {
        [Required]
        [SerializeField] 
        private GameObject gameOverPanel;
        
        [Required]
        [SerializeField] 
        private TextMeshProUGUI titleText;
        
        [Required]
        [SerializeField] 
        private TextMeshProUGUI descriptionText;
        
        [Required]
        [SerializeField] 
        private ButtonProvider restartButton;
        
        public event Action OnRestartClicked = delegate { };
        
        private void Awake()
        {
            restartButton.OnButtonClicked += HandleRestartClicked;
        }
        
        private void OnDestroy()
        {
            restartButton.OnButtonClicked -= HandleRestartClicked;
        }
        
        private void HandleRestartClicked()
        {
            OnRestartClicked.Invoke();
        }
        
        public void ShowGameOver()
        {
            SetActive(true);
            
            //TODO: Вынести текст в SO?
            titleText.SetText("Игра окончена!");
            descriptionText.SetText("Ваш баланс исчерпан. Попробуйте начать заново.");
        }
        
        public void HideGameOver()
        {
            SetActive(false);
        }
        
        private void SetActive(bool state)
        {
            gameOverPanel.SetActive(state);
        }
    }
} 
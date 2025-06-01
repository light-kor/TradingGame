using TMPro;
using TriInspector;
using UnityEngine;

namespace Core.UI.Providers
{
    public class PlayerMoneyProvider : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private TextMeshProUGUI valueText;

        public void UpdateValueText(float value)
        {
            string newText = $"{value:F2} $";
            valueText.SetText(newText);
        }
    }
}
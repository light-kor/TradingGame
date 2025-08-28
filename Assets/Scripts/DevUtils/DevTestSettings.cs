using System;
using UnityEngine;

namespace DevUtils
{
    /// <summary>
    /// Настройки для тестирования различных систем игры
    /// </summary>
    [CreateAssetMenu(fileName = "DevTestSettings", menuName = "Settings/DevTestSettings")]
    public class DevTestSettings : ScriptableObject
    {
        [field: Header("Price Movement Override")]
        [field: SerializeField]
        public PriceMoveOverrideType ForcedCandleDirection { get; private set; } 
        
        /// <summary>
        /// Проверяет, активны ли настройки для переопределения движения цены
        /// </summary>
        public bool IsPriceOverrideActive()
        {
            return ForcedCandleDirection != PriceMoveOverrideType.None;
        }
        
        /// <summary>
        /// Получает принудительное направление движения цены для свечей
        /// </summary>
        public bool GetForcedCandleDirection()
        {
            return ForcedCandleDirection switch
            {
                PriceMoveOverrideType.AlwaysLong => true,
                PriceMoveOverrideType.AlwaysShort => false,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}

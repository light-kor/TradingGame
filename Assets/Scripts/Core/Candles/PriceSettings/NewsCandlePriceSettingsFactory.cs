using Settings;
using UnityEngine;
using Zenject;

namespace Core.Candles.PriceSettings
{
    public class NewsCandlePriceSettingsFactory
    {
        [Inject] private readonly CandlePriceSettingsFactory _originalFactory;
        [Inject] private readonly NewsCandlesRepository _newsCandlesRepository;
        
        public bool TryCreateNewsCandlePriceSettings(float openPrice, out NewsCandlePriceSettings settings)
        {
            settings = null;
            var newsCandleInfo = _newsCandlesRepository.GetRandomNewsCandle();
            
            if (newsCandleInfo == null)
                return false;
            
            // Создаем базовые настройки паттерна
            var baseSettings = _originalFactory.CreateCandlePriceSettings(openPrice);
            
            // Применяем новостной множитель к базовым значениям паттерна
            float bodyChange = Mathf.Abs(baseSettings.ClosePrice - baseSettings.OpenPrice) * newsCandleInfo.VolatilityMultiplier;
            float wickChange = Mathf.Abs(baseSettings.HighPrice - baseSettings.LowPrice - Mathf.Abs(baseSettings.ClosePrice - baseSettings.OpenPrice)) * newsCandleInfo.VolatilityMultiplier;
            
            float closePrice, highPrice, lowPrice;
            bool isLong = newsCandleInfo.IsLong; // Направление определяется новостью
            
            if (isLong)
            {
                closePrice = openPrice + bodyChange;
                highPrice = closePrice + wickChange;
                lowPrice = openPrice - wickChange;
            }
            else
            {
                closePrice = openPrice - bodyChange;
                highPrice = openPrice + wickChange;
                lowPrice = closePrice - wickChange;
                
                closePrice = Mathf.Max(closePrice, 0f);
                highPrice = Mathf.Max(highPrice, 0f);
                lowPrice = Mathf.Max(lowPrice, 0f);
            }
            
            Debug.LogError($"НОВОСТЬ: {newsCandleInfo.Title}\n" +
                           $"{newsCandleInfo.Description}" +
                           $"Множитель: {newsCandleInfo.VolatilityMultiplier:F1}x, Направление: {(isLong ? "ЛОНГ" : "ШОРТ")}" +
                           $"Новостная свеча: Open={openPrice:F2}, Close={closePrice:F2}, High={highPrice:F2}, Low={lowPrice:F2}");
            
            settings = new NewsCandlePriceSettings(openPrice, closePrice, highPrice, lowPrice, isLong);
            settings.SetNewsData(newsCandleInfo.Title, newsCandleInfo.Description);
            return true;
        }
    }
} 
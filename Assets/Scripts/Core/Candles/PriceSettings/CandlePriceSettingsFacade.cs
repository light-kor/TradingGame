using Settings;
using UnityEngine;
using Zenject;

namespace Core.Candles.PriceSettings
{
    public class CandlePriceSettingsFacade
    {
        [Inject] private readonly CandlePriceSettingsFactory _normalFactory;
        [Inject] private readonly NewsCandlePriceSettingsFactory _newsFactory;
        [Inject] private readonly NewsCandlesRepository _newsCandlesRepository;
        
        public CandlePriceSettings CreateCandlePriceSettings(float openPrice)
        {
            if (ShouldSpawnNewsCandle())
            {
                bool hasNews = _newsFactory.TryCreateNewsCandlePriceSettings(openPrice, out NewsCandlePriceSettings settings);

                if (hasNews)
                {
                    //TODO: Show news popup
                    return settings;
                }
            }
            
            return _normalFactory.CreateCandlePriceSettings(openPrice);
        }

        private bool ShouldSpawnNewsCandle()
        {
            return Random.Range(0f, 1f) < _newsCandlesRepository.NewsCandleChance;
        }
    }
} 
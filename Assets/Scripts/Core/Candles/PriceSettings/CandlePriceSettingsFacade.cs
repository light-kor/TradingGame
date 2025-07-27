using Core.UI.Presenters;
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
        [Inject] private readonly NewsPopupPresenter _newsPopupPresenter;
        
        public CandlePriceSettings CreateRandomCandlePriceSettings(float openPrice)
        {
            if (ShouldSpawnNewsCandle())
            {
                bool hasNews = _newsFactory.TryCreateNewsCandlePriceSettings(openPrice, out NewsCandlePriceSettings settings);

                if (hasNews)
                {
                    _newsPopupPresenter.ShowNewsPopup(settings);
                    return settings;
                }
            }
            
            return CreateDefaultCandlePriceSettings(openPrice);
        }
        
        public CandlePriceSettings CreateDefaultCandlePriceSettings(float openPrice)
        {
            return _normalFactory.CreateCandlePriceSettings(openPrice);
        }

        private bool ShouldSpawnNewsCandle()
        {
            return Random.Range(0f, 1f) < _newsCandlesRepository.NewsCandleChance;
        }
    }
} 
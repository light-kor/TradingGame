using Settings;
using UnityEngine;

namespace Core.Candles
{
    public class CandlePresenter
    {
        private readonly GameSettings _gameSettings;
        public CandlePriceSettings PriceSettings { get; private set; }
        public CandleProvider Provider { get; private set; }
        
        public CandlePresenter(GameSettings settings, CandleProvider provider)
        {
            _gameSettings = settings;
            Provider = provider;
        }
        
        public void PrepareProvider()
        {
            Provider.SetActive(true);
            Provider.ResetCandleSize(_gameSettings.BodyWidth, _gameSettings.WickWidth);
        }
        
        public void SetPriceSettings(CandlePriceSettings priceSettings)
        {
            PriceSettings = priceSettings;
        }

        public void SetPosition(int xPos, float lastCurrentClosePrice)
        {
            Provider.SetPosition(new Vector3(xPos * _gameSettings.CandleSpawnOffset, lastCurrentClosePrice, 0));
        }
    }
}
using UnityEngine;

namespace Core.Candles
{
    public class CandlePresenter
    {
        public CandlePriceSettings PriceSettings { get; private set; }
        public GameSettings Settings { get; private set; }
        public CandleProvider Provider { get; private set; }

        public CandlePresenter(CandlePriceSettings priceSettings, GameSettings settings)
        {
            PriceSettings = priceSettings;
            Settings = settings;
        }
        
        public void SetProvider(CandleProvider provider)
        {
            Provider = provider;
        }

        public void SetPosition(int xPos, float lastCurrentClosePrice)
        {
            Provider.SetPosition(new Vector3(xPos * Settings.CandleSpawnOffset, lastCurrentClosePrice, 0));
        }
    }
}
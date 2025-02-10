namespace Core.Candles
{
    public class CandlePriceSettings
    {
        public float OpenPrice { get; private set; }
        public float ClosePrice { get; private set; }
        public float HighPrice { get; private set; }
        public float LowPrice { get; private set; }
        public bool IsLong { get; private set; }

        public CandlePriceSettings(float openPrice, float closePrice, float highPrice, float lowPrice, bool isLong)
        {
            OpenPrice = openPrice;
            ClosePrice = closePrice;
            HighPrice = highPrice;
            LowPrice = lowPrice;
            IsLong = isLong;
        }
    }
}
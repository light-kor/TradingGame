namespace Core.Candles
{
    public class NewsCandlePriceSettings : CandlePriceSettings
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        
        public NewsCandlePriceSettings(float openPrice, float closePrice, float highPrice, float lowPrice, bool isLong) 
            : base(openPrice, closePrice, highPrice, lowPrice, isLong)
        {
        }

        public void SetNewsData(string title, string description)
        {
            Title = title;
            Description = description;
        }
    }
}
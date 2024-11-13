using UnityEngine;

public class CandleController
{
    public void CreateCandle(float openPrice, float closePrice, float highPrice, float lowPrice, Color longColor, Color shortColor, SpriteRenderer bodySpriteRenderer, SpriteRenderer wickSpriteRenderer)
    {
        // Set initial properties of body and wick
        UpdateCandleAppearance(bodySpriteRenderer, wickSpriteRenderer, openPrice, closePrice, highPrice, lowPrice, longColor, shortColor);
    }

    private void UpdateCandleAppearance(SpriteRenderer body, SpriteRenderer wick, float openPrice, float closePrice, float highPrice, float lowPrice, Color longColor, Color shortColor)
    {
        // Determine if candle is long or short
        bool isLong = closePrice > openPrice;
        Color candleColor = isLong ? longColor : shortColor;
        body.color = candleColor;
        wick.color = candleColor;

        // Set the scale of the candle body
        float bodyHeight = Mathf.Abs(closePrice - openPrice) / 5;
        body.transform.localScale = new Vector3(0.08f, bodyHeight, 0.2f);

        // Set the scale of the candle wick
        float wickHeight = (highPrice - lowPrice) / 5;
        wick.transform.localScale = new Vector3(0.02f, wickHeight, 0.2f);
    }
}
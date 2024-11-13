using UnityEngine;
using DG.Tweening;

public class CandleProvider : MonoBehaviour
{
    public float openPrice;
    public float closePrice;
    public float highPrice;
    public float lowPrice;
    public Color longColor = Color.green;
    public Color shortColor = Color.red;
    public float animationDuration = 1.0f;

    public SpriteRenderer bodySpriteRenderer;
    public SpriteRenderer wickSpriteRenderer;

    private void Start()
    {
        // Create a new candle instance using CandleController
        CandleController candleController = new CandleController();
        candleController.CreateCandle(openPrice, closePrice, highPrice, lowPrice, longColor, shortColor, bodySpriteRenderer, wickSpriteRenderer);
        AnimateCandle();
    }

    private void AnimateCandle()
    {
        // Animate the candle to show movement using DOTween
        float bodyHeight = Mathf.Abs(closePrice - openPrice) / 5;
        bodySpriteRenderer.transform.DOScaleY(bodyHeight, animationDuration).SetEase(Ease.OutQuad);

        float wickHeight = (highPrice - lowPrice) / 5;
        wickSpriteRenderer.transform.DOScaleY(wickHeight, animationDuration).SetEase(Ease.OutQuad);
    }
}
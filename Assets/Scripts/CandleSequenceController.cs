using DG.Tweening;
using UnityEngine;

public class CandleSequenceController : MonoBehaviour
{
    private const int SCALE_FACTOR = 5;
    
    public SpriteRenderer bodyPrefab;
    public SpriteRenderer wickPrefab;
    public Color longColor = Color.green;
    public Color shortColor = Color.red;
    public float animationDuration = 1.0f;

    private float currentClosePrice;

    void Start()
    {
        // Initialize with a random starting price
        currentClosePrice = Random.Range(1.0f, 5.0f);
        StartCoroutine(SpawnCandleSequence(10));
    }

    private System.Collections.IEnumerator SpawnCandleSequence(int candleCount)
    {
        for (int i = 0; i < candleCount; i++)
        {
            // Generate random price changes
            float openPrice = currentClosePrice;
            float closePrice = openPrice + Random.Range(-2.0f, 2.0f);
            float highPrice = Mathf.Max(openPrice, closePrice) + Random.Range(0.2f, 1.0f);
            float lowPrice = Mathf.Min(openPrice, closePrice) - Random.Range(0.1f, 0.5f);

            // Instantiate new body and wick sprites
            SpriteRenderer newBody = Instantiate(bodyPrefab, new Vector3(i * 0.12f, currentClosePrice / SCALE_FACTOR, 0), Quaternion.identity);
            SpriteRenderer newWick = Instantiate(wickPrefab, new Vector3(i * 0.12f, currentClosePrice / SCALE_FACTOR, 0), Quaternion.identity);

            // Create a new candle instance using CandleController
            CandleController candleController = new CandleController();
            candleController.CreateCandle(openPrice, closePrice, highPrice, lowPrice, longColor, shortColor, newBody, newWick);

            // Animate the candle
            AnimateCandle(newBody, newWick, openPrice, closePrice, highPrice, lowPrice);

            // Update the current close price for the next candle
            currentClosePrice = closePrice;

            yield return new WaitForSeconds(animationDuration);
        }
    }

    private void AnimateCandle(SpriteRenderer body, SpriteRenderer wick, float openPrice, float closePrice, float highPrice, float lowPrice)
    {
        // Animate the candle to show movement using DOTween
        float bodyHeight = Mathf.Abs(closePrice - openPrice) / SCALE_FACTOR;
        body.transform.DOScaleY(bodyHeight, animationDuration).SetEase(Ease.OutQuad);

        float wickHeight = (highPrice - lowPrice) / SCALE_FACTOR;
        wick.transform.DOScaleY(wickHeight, animationDuration).SetEase(Ease.OutQuad);
    }
}

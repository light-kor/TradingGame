using System.Collections;
using Core.Pool;
using Core.UI;
using Settings;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace Core.Candles
{
    public class CandleSequenceController : MonoBehaviour
    {
        [SerializeField] 
        private ButtonProvider continueButton;
        
        [SerializeField] 
        private ButtonProvider cameraMoveButton;
        
        [Inject] private readonly CandlePriceSettingsFactory _candlePriceSettingsFactory;
        [Inject] private readonly CandlePresenterFactory _candlePresenterFactory;
        [Inject] private readonly CandleAnimationFacade _candleAnimationFacade;
        [Inject] private readonly CameraMoveController _cameraMoveController;
        [Inject] private readonly CandleProviderPool _candleProviderPool;
        [Inject] private readonly GameSettings _settings;

        private CandleProvider _lastCandleProvider;
        private float _currentClosePrice;
        private int _currentXPosition;

        private void Start()
        {
            Assert.IsTrue(_settings.CandlesSpawnCount > 0);
            
            continueButton.OnButtonClicked += SpawnCandles;
            cameraMoveButton.OnButtonClicked += UpdateCameraPosition;

            StartCoroutine(_candleProviderPool.InitializePool(() =>
            {
                StartCoroutine(SpawnCandleSequence(_settings.CandlesSpawnCount, true));
            }));
        }
        
        private void OnDestroy()
        {
            continueButton.OnButtonClicked -= SpawnCandles;
            cameraMoveButton.OnButtonClicked -= UpdateCameraPosition;
        }
        
        private void SpawnCandles()
        {
            StartCoroutine(SpawnCandleSequence(_settings.CandlesSpawnCount));
        }

        private IEnumerator SpawnCandleSequence(int candleCount, bool instantlySpawn = false)
        {
            for (int i = 0; i < candleCount; i++)
            {
                var candle = _candlePresenterFactory.GetFreeCandlePresenter();
                candle.PrepareProvider();
                
                var candlePriceSettings = _candlePriceSettingsFactory.CreateCandlePriceSettings(_currentClosePrice);
                candle.SetPriceSettings(candlePriceSettings);
                candle.SetPosition(_currentXPosition, _currentClosePrice);
                
                yield return _candleAnimationFacade.AnimateCandle(candle, instantlySpawn);

                _lastCandleProvider = candle.Provider;
                _currentClosePrice = candle.PriceSettings.ClosePrice;
                _currentXPosition++;
                
                if (instantlySpawn == false)
                    UpdateCameraPosition(false);
            }

            UpdateCameraPosition(instantlySpawn);
        }
        
        private void UpdateCameraPosition()
        {
            UpdateCameraPosition(false);
        }

        private void UpdateCameraPosition(bool instantlySpawn)
        {
            //TODO: Оно приведёт нас в центр свечи? Или не факт. Мб надо += _currentClosePrice
            var lastCandlePosition = _lastCandleProvider.transform.position;
            
            if (instantlySpawn)
                _cameraMoveController.MoveCameraInstantly(lastCandlePosition);
            else
                _cameraMoveController.MoveCameraWithAnimation(lastCandlePosition);
        }
    }
}
using Core.Candles;
using Core.UI.Providers;
using Settings;
using UnityEngine;
using Zenject;

namespace Core.UI.Presenters
{
    public class CurrentPositionPresenter : IInitializable
    {
        [Inject] private readonly CoreMainPanelProvider _mainPanelProvider;
        [Inject] private readonly CoreEventBus _coreEventBus;
        [Inject] private readonly GameSettings _settings;
        
        private CurrentPositionProvider _currentPositionProvider;
        
        public float EntryPrice { get; private set; }
        public int InitialMargin { get; private set; }
        public int Pnl { get; private set; }
        public bool IsLong { get; private set; }
        
        public void Initialize()
        {
            _currentPositionProvider = _mainPanelProvider.CurrentPositionProvider;
            _currentPositionProvider.SetActive(false);
        }
        
        public void InitPosition(int margin, float entryPrice, bool isLong)
        {
            InitialMargin = margin;
            EntryPrice = entryPrice;
            IsLong = isLong;
            
            _coreEventBus.OnCurrentPriceUpdated += UpdatePosition;
            _currentPositionProvider.SetPositionMargin(margin);
            _currentPositionProvider.SetActive(true);
        }

        private void UpdatePosition(CandlePresenter currentCandle)
        {
            float priceRatio = currentCandle.CurrentPrice / EntryPrice;
            float priceDiffRatio = IsLong ? priceRatio - 1f : 1f - priceRatio;
            
            int pnl = (int)(InitialMargin * priceDiffRatio);
            float roi = priceDiffRatio * 100f;
            Pnl = pnl;
            
            if (roi < -100f)
                Debug.LogError("AAAAAAAA Пизда рулю");
            
            Color roiColor = roi < 0 ? _settings.ShortColor : _settings.LongColor;
            
            _currentPositionProvider.UpdateCurrentPnl(pnl);
            _currentPositionProvider.UpdateCurrentRoi(roi, roiColor);
        }

        public void ClosePosition()
        {
            _coreEventBus.OnCurrentPriceUpdated -= UpdatePosition;
        }
    }
}
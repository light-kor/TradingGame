using System;
using Core.Candles;
using Core.TradePosition.Close;
using Core.UI.Presenters;
using Zenject;

namespace Core.TradePosition
{
    public class CurrentPositionController : IInitializable, IDisposable
    {
        [Inject] private readonly PositionSizeSelectPresenter _positionSizeSelectPresenter;
        [Inject] private readonly CurrentPositionPresenter _currentPositionPresenter;
        [Inject] private readonly CandleSequenceController _candleSequenceController;
        [Inject] private readonly PositionClosePresenter _positionClosePresenter;
        [Inject] private readonly PositionCloseChecker _positionCloseChecker;
        [Inject] private readonly BalancePresenter _balancePresenter;
        [Inject] private readonly CoreEventBus _coreEventBus;
        
        public float EntryPrice { get; private set; }
        public int InitialMargin { get; private set; }
        public int Pnl { get; private set; }
        public float Roi { get; private set; }
        public bool IsLong { get; private set; }
        public bool IsOpen { get; private set; }
        
        public void Initialize()
        {
            _currentPositionPresenter.OnOpenPositionClicked += OpenPosition;
            _currentPositionPresenter.OnClosePositionClicked += ManualClosePosition;
            _coreEventBus.OnCurrentPriceUpdated += UpdatePosition;
        }

        public void Dispose()
        {
            _currentPositionPresenter.OnOpenPositionClicked -= OpenPosition;
            _currentPositionPresenter.OnClosePositionClicked -= ManualClosePosition;
            _coreEventBus.OnCurrentPriceUpdated -= UpdatePosition;
        }

        private void OpenPosition(bool isLong)
        {
            int margin = _positionSizeSelectPresenter.PositionSize;

            //TODO: Тут добавить потом проверку на минимальный размер позиции
            if (margin <= 0)
                return;
            
            InitPosition(margin, isLong);
            
            _coreEventBus.FirePositionOpened();
            _balancePresenter.DecreaseBalance(margin);
            _candleSequenceController.StartSpawnCandles();
        }
        
        private void InitPosition(int margin, bool isLong)
        {
            float currentPrice = _candleSequenceController.CurrentCandlePresenter.CurrentPrice;
            
            InitialMargin = margin;
            EntryPrice = currentPrice;
            IsLong = isLong;
            IsOpen = true;
            
            _currentPositionPresenter.OpenPosition(margin);
        }
        
        private void UpdatePosition(CandlePresenter currentCandle)
        {
            if (!IsOpen)
                return;
            
            float priceRatio = currentCandle.CurrentPrice / EntryPrice;
            float priceDiffRatio = IsLong ? priceRatio - 1f : 1f - priceRatio;
            
            Pnl = (int)(InitialMargin * priceDiffRatio);
            Roi = priceDiffRatio * 100f;
            
            _currentPositionPresenter.UpdatePosition(Pnl, Roi);

            if (_positionCloseChecker.CheckPositionCloseConditions(currentCandle, out var closeType))
                ClosePosition(closeType);
        }
        
        private void ClosePosition(PositionCloseType closeType)
        {
            if (!IsOpen)
                return;
                
            IsOpen = false;
            
            int result = InitialMargin + Pnl;
            _balancePresenter.IncreaseBalance(result);
            _currentPositionPresenter.ClosePosition();
            _candleSequenceController.StopSpawn();
            
            _positionClosePresenter.ShowPositionClose(closeType, Pnl);
            _coreEventBus.FirePositionClosed(closeType);
        }
        
        private void ManualClosePosition()
        {
            ClosePosition(PositionCloseType.Manual);
        }
    }
}
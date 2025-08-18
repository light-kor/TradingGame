using System;
using Core.Candles;
using Core.TradePosition.Close;

namespace Core
{
    public class CoreEventBus
    {
        public event Action<CandlePresenter> OnCurrentPriceUpdated = delegate {  };
        public event Action OnCameraMoved = delegate {  };
        public event Action OnPositionOpened = delegate {  };
        public event Action<PositionCloseType> OnPositionClosed = delegate {  };
        public event Action OnClosePositionPanelShown = delegate {  };
        public event Action OnGameOverPanelShown = delegate {  };

        public void FireCurrentPriceUpdated(CandlePresenter currentCandle)
        {
            OnCurrentPriceUpdated.Invoke(currentCandle);
        }
        
        public void FireCameraMoved()
        {
            OnCameraMoved.Invoke();
        }
        
        public void FirePositionOpened()
        {
            OnPositionOpened.Invoke();
        }
        
        public void FirePositionClosed(PositionCloseType closeType)
        {
            OnPositionClosed.Invoke(closeType);
        }
        
        public void FireClosePositionPanelShown()
        {
            OnClosePositionPanelShown.Invoke();
        }
        
        public void FireOnGameOverPanelShown()
        {
            OnGameOverPanelShown.Invoke();
        }
    }
}
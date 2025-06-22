using System;
using Core.Candles;

namespace Core
{
    public class CoreEventBus
    {
        public event Action<CandlePresenter> OnCurrentPriceUpdated = delegate {  };
        public event Action OnCameraMoved = delegate {  };
        public event Action OnPositionOpened = delegate {  };
        public event Action OnPositionClosed = delegate {  };

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
        
        public void FirePositionClosed()
        {
            OnPositionClosed.Invoke();
        }
    }
}
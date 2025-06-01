using System;
using Core.Candles;

namespace Core
{
    public class CoreEventBus
    {
        public event Action<CandlePresenter> OnCandleSpawned = delegate {  };
        public event Action OnCameraMoved = delegate {  };

        public void FireCurrentPriceUpdated(CandlePresenter currentCandle)
        {
            OnCandleSpawned.Invoke(currentCandle);
        }
        
        public void FireCameraMoved()
        {
            OnCameraMoved.Invoke();
        }
    }
}
using System;
using Core.Candles;

namespace Core
{
    public class CoreEventBus
    {
        public event Action<CandlePresenter> OnCurrentPriceUpdated = delegate {  };
        public event Action OnNeedUpdatePriceLineByCameraMove = delegate {  };

        public void FireCurrentPriceUpdated(CandlePresenter currentCandle)
        {
            OnCurrentPriceUpdated.Invoke(currentCandle);
        }
        
        public void FireNeedUpdatePriceLineByCameraMove()
        {
            OnNeedUpdatePriceLineByCameraMove.Invoke();
        }
    }
}
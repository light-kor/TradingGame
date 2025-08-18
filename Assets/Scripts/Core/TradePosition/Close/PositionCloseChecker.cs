using Core.Candles;
using Zenject;

namespace Core.TradePosition.Close
{
    public class PositionCloseChecker
    {
        private const float Zero = 0f;
        private const float LiquidationRoi = -100f;
        
        [Inject] private readonly CurrentPositionController _currentPositionController;
        
        public bool CheckPositionCloseConditions(CandlePresenter currentCandle, out PositionCloseType positionCloseType)
        {
            if (IsCoinBankrupt(currentCandle))
            {
                positionCloseType = PositionCloseType.CoinBankrupt;
                return true;
            }
            
            if (PositionLiquidated())
            {
                positionCloseType = PositionCloseType.PositionLiquidated;
                return true;
            }

            positionCloseType = PositionCloseType.None;
            return false;
        }

        private bool IsCoinBankrupt(CandlePresenter currentCandle)
        {
            return currentCandle.CurrentPrice <= Zero;
        }
        
        private bool PositionLiquidated()
        {
            return _currentPositionController.Roi <= LiquidationRoi;
        }
    }
}
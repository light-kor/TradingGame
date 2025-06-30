using Core.Candles;
using Zenject;

namespace Core
{
    public class CoinInitializeFacade
    {
        [Inject] private readonly CandleSequenceController _candleSequenceController;
        [Inject] private readonly CurrentCoinFacade _currentCoinFacade;
        
        public void InitializeRandomCoin()
        {
            _currentCoinFacade.CreateCurrentCoin();
            var coinPattern = _currentCoinFacade.GetCurrentPattern();
            
            _candleSequenceController.InitializeCoin(coinPattern.InitialPrice);
        }
    }
}
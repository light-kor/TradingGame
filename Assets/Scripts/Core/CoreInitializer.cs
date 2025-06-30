using Core.Candles;
using Core.Pool;
using Zenject;

namespace Core
{
    public class CoreInitializer : IInitializable
    {
        [Inject] private readonly CandleSequenceController _candleSequenceController;
        [Inject] private readonly CandleProviderPool _candleProviderPool;
        [Inject] private readonly CurrentCoinFacade _currentCoinFacade;
        
        public void Initialize()
        {
            _currentCoinFacade.CreateCurrentCoin();
            var coinPattern = _currentCoinFacade.GetCurrentPattern();
            
            InitializeCandles(coinPattern.InitialPrice);
        }

        private async void InitializeCandles(float initialPrice)
        {
            await _candleProviderPool.InitializePoolAsync();
            _candleSequenceController.InitializeCoin(initialPrice);
        }
    }
}
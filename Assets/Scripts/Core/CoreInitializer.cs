using Core.Pool;
using Zenject;

namespace Core
{
    public class CoreInitializer : IInitializable
    {
        [Inject] private readonly CoinInitializeFacade _coinInitializeFacade;
        [Inject] private readonly CandleProviderPool _candleProviderPool;
        
        public void Initialize()
        {
            InitializeFirstCoin();
        }

        private async void InitializeFirstCoin()
        {
            await _candleProviderPool.InitializePoolAsync();
            _coinInitializeFacade.InitializeRandomCoin();
        }
    }
}
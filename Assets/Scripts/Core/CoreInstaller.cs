using Common.Player;
using Core.Candles;
using Core.Candles.SpawnFacade;
using Core.Pool;
using UnityEngine;
using Zenject;

namespace Core
{
    public class CoreInstaller : MonoInstaller
    {
        [SerializeField] 
        private CandleProvidersContainer candleProvidersContainer;
        
        [SerializeField] 
        private Camera mainCamera;
        
        [SerializeField] 
        private FundSourceRepository fundSourceRepository;
        
        [SerializeField] 
        private MoneyProvider moneyProvider;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MoneyFacade>()
                .AsSingle();

            Container.Bind<FundSourceRepository>()
                .FromInstance(fundSourceRepository)
                .AsSingle();
            
            Container.Bind<MoneyProvider>()
                .FromInstance(moneyProvider)
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<CoreEventBus>()
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<CandlePresenterFactory>()
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<CandlePriceSettingsFactory>()
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<CandleSpawnAnimationFacade>()
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<CandleSpawnInstantlyFacade>()
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<CameraMoveController>()
                .AsSingle()
                .WithArguments(mainCamera);

            Container.BindInterfacesAndSelfTo<CandleProviderPool>()
                .AsSingle()
                .WithArguments(candleProvidersContainer);
        }
    }
}

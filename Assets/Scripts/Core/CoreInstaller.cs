using Common.Player;
using Core.Candles;
using Core.Candles.SpawnFacade;
using Core.Pool;
using TriInspector;
using UnityEngine;
using Zenject;

namespace Core
{
    public class CoreInstaller : MonoInstaller
    {
        [Required]
        [SerializeField] 
        private CandleProvidersContainer candleProvidersContainer;
        
        [Required]
        [SerializeField] 
        private Camera mainCamera;
        
        [Required]
        [SerializeField] 
        private FundSourceRepository fundSourceRepository;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MoneyFacade>()
                .AsSingle();

            Container.Bind<FundSourceRepository>()
                .FromInstance(fundSourceRepository)
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<CoreEventBus>()
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<CoreEntryController>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<CandleSequenceController>()
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

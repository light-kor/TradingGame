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
        
        public override void InstallBindings()
        {
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

using Core.Candles;
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
            Container.BindInterfacesAndSelfTo<CandlePresenterFactory>()
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<CandlePriceSettingsFactory>()
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<CandleAnimationFacade>()
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

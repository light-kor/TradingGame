using Core.Candles;
using Core.Pool;
using Core.UI;
using UnityEngine;
using Zenject;

namespace Core
{
    public class CoreInstaller : MonoInstaller
    {
        [SerializeField] 
        private GameSettings gameSettings;
        
        [SerializeField] 
        private ContinueButtonProvider continueButtonProvider;
       
        [SerializeField] 
        private CandleProvidersContainer candleProvidersContainer;
        
        public override void InstallBindings()
        {
            Container.BindInstance(gameSettings).AsSingle();
            
            Container.BindInterfacesAndSelfTo<CandlePresenterFactory>()
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<CandlePriceSettingsFactory>()
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<CandleAnimationFacade>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<CandleProviderPool>()
                .AsSingle()
                .WithArguments(candleProvidersContainer);
            
            Container.Bind<ContinueButtonProvider>()
                .FromInstance(continueButtonProvider)
                .AsSingle();
        }
    }
}

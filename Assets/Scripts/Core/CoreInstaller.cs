using Common.Player;
using Core.Candles;
using Core.Candles.PriceSettings;
using Core.Candles.SpawnFacade;
using Core.GameOver;
using Core.Pool;
using Core.TradePosition;
using Core.TradePosition.Close;
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

            Container.BindInterfacesAndSelfTo<CurrentPositionController>()
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<PositionCloseChecker>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<CandleSequenceController>()
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<CandlePresenterFactory>()
                .AsSingle();
            
            Container.BindFactory<CandleProvider, CandlePresenter, CandlePresenterZenjectFactory>();
            
            Container.BindInterfacesAndSelfTo<CandlePriceSettingsFactory>()
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<NewsCandlePriceSettingsFactory>()
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<CandlePriceSettingsFacade>()
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<CandleSpawnAnimationFacade>()
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<CandleSpawnInstantlyFacade>()
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<GameOverController>()
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<CameraMoveController>()
                .AsSingle()
                .WithArguments(mainCamera);

            Container.BindInterfacesAndSelfTo<CandleProviderPool>()
                .AsSingle()
                .WithArguments(candleProvidersContainer);
            
            Container.BindInterfacesAndSelfTo<CurrentCoinFacade>()
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<CoreInitializer>()
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<CoinInitializeFacade>()
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<ChangeCoinHandler>()
                .AsSingle();
        }
    }
}
